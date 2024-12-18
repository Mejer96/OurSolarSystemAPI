using OurSolarSystemAPI.Repository.MySQL;
using OurSolarSystemAPI.Repository.MongoDB;
using OurSolarSystemAPI.Models;
using OurSolarSystemAPI.Models.MongoDB;

namespace OurSolarSystemAPI.Service.MongoDB 
{
    public class MigrationServiceMongoDB 
    {

        private readonly PlanetRepositoryMySQL _planetRepoMySQL;
        private readonly BarycenterRepositoryMySQL _barycenterRepoMySQL;
        private readonly MoonRepositoryMySQL _moonRepoMySQL;
        private readonly BarycenterRepositoryMongoDB _barycenterRepoMongoDB;
        private readonly PlanetRepositoryMongoDB _planetRepoMongoDB;
        private readonly MoonRepositoryMongoDB _moonRepoMongoDB;
        private readonly EphemerisRepositoryMongoDB _ephemerisRepoMongoDB;
        private readonly ArtificialSatelliteRepositoryMySQL _artificialSatelliteRepoMySQL;
        private readonly ArtificialSatelliteRepositoryMongoDB _artificialSatelliteRepoMongoDB;
    

        public MigrationServiceMongoDB(
            PlanetRepositoryMySQL planetRepoMySQL,
            BarycenterRepositoryMySQL barycenterRepoMySQL,
            MoonRepositoryMySQL moonRepoMySQL,
            ArtificialSatelliteRepositoryMySQL artificialSatelliteRepoMySQL,
            BarycenterRepositoryMongoDB barycenterRepositoryMongoDB,
            PlanetRepositoryMongoDB planetRepoMongoDB,
            MoonRepositoryMongoDB moonRepoMongoDB,
            EphemerisRepositoryMongoDB ephemerisRepoMongoDB,
            ArtificialSatelliteRepositoryMongoDB artificialSatelliteRepoMongoDB
            ) 
        {
            _planetRepoMySQL = planetRepoMySQL;
            _barycenterRepoMySQL = barycenterRepoMySQL;
            _moonRepoMySQL = moonRepoMySQL;
            _artificialSatelliteRepoMySQL = artificialSatelliteRepoMySQL;
            _barycenterRepoMongoDB = barycenterRepositoryMongoDB;
            _planetRepoMongoDB = planetRepoMongoDB;
            _moonRepoMongoDB = moonRepoMongoDB;
            _ephemerisRepoMongoDB = ephemerisRepoMongoDB;
            _artificialSatelliteRepoMongoDB = artificialSatelliteRepoMongoDB;
        }



        public async Task MigrateBarycenters() 
        {
            List<Barycenter> barycenters = await _barycenterRepoMySQL.requestAllBarycentersWithEphemeris();
            List<Planet> planets = await _planetRepoMySQL.requestAllPlanetsWithMoons();

            foreach (var barycenter in barycenters) 
            {
                if (barycenter.Ephemeris is null) throw new Exception($"No ephemeris data for planet with horizon id: {barycenter.HorizonId}");


               BarycenterMongoDTO barycenterDTO = BarycenterMongoDTO.ConvertToMongoDTO(barycenter);

               foreach (var planet in planets) 
               {
                    if (planet.BarycenterHorizonId == barycenter.HorizonId) 
                    {
                        barycenterDTO.Planet = PlanetMongoDTO.ConvertToMongoDTO(planet);
                    }

               }
               await _barycenterRepoMongoDB.CreateBarycenterAsync(barycenterDTO);
               var ephemerisDTOs = new List<EphemerisMongoDTO>();
               
               foreach (var ephemeris in barycenter.Ephemeris) 
               {
                    ephemerisDTOs.Add(EphemerisMongoDTO.ConvertToEphemerisMongoDTO(ephemeris, barycenter.HorizonId, barycenterDTO.Id));
               }

               await _ephemerisRepoMongoDB.CreateBarycenterEphemerisManyAsync(ephemerisDTOs);

            }
        }

        public async Task MigrateSun() 
        {
            Star sun = await _planetRepoMySQL.RequestSunWithEphemeris();

            var ephemerisDTOs = new List<EphemerisMongoDTO>();
            if (sun.Ephemeris is null) throw new Exception($"No ephemeris data for sun with horizon id: {sun.HorizonId}");
            StarMongoDTO sunDTO = StarMongoDTO.ConvertToStarMongoDTO(sun);

            await _planetRepoMongoDB.CreateSunAsync(sunDTO);

            foreach (EphemerisSun ephemeris in sun.Ephemeris) 
            {
                ephemerisDTOs.Add(EphemerisMongoDTO.ConvertToEphemerisMongoDTO(ephemeris, sun.HorizonId, sunDTO.Id));
            }
            await _ephemerisRepoMongoDB.CreatePlanetEphemerisManyAsync(ephemerisDTOs);
        }

        public async Task MigratePlanets() 
        {
            List<Planet> planets = await _planetRepoMySQL.requestAllPlanetsWithEphemerisAndMoons();

            foreach (var planet in planets) 
            {
                if (planet.Ephemeris is null) throw new Exception($"No ephemeris data for planet with horizon id: {planet.HorizonId}");
                PlanetMongoDTO planetDTO = PlanetMongoDTO.ConvertToMongoDTO(planet);
                var moonDTOs = new List<MoonMongoDTO>();
                
                if (planet.Moons != null)
                {
                    foreach (Moon moon in planet.Moons)
                    {
                        moonDTOs.Add(MoonMongoDTO.ConvertToMoonMongoDTO(moon));
                    }
                }
                planetDTO.Moons = moonDTOs;
                await _planetRepoMongoDB.CreatePlanetAsync(planetDTO);
                var ephemerisDTOs = new List<EphemerisMongoDTO>();

                foreach (EphemerisPlanet ephemeris in planet.Ephemeris) 
                {
                    ephemerisDTOs.Add(EphemerisMongoDTO.ConvertToEphemerisMongoDTO(ephemeris, planet.HorizonId, planetDTO.Id));
                }

                await _ephemerisRepoMongoDB.CreatePlanetEphemerisManyAsync(ephemerisDTOs);

            }
        }

        public async Task MigrateMoons() 
        {
            List<Moon> moons = await _moonRepoMySQL.requestAllMoonsWithEphemeris();
            var moonDTOs = new List<MoonMongoDTOWithId>();

            foreach (var moon in moons) 
            {
                if (moon.Ephemeris is null) throw new Exception($"No ephemeris data for moon with horizon id: {moon.HorizonId}");
                MoonMongoDTOWithId moonDTO = MoonMongoDTOWithId.ConvertToMoonMongoDTO(moon);
                await _moonRepoMongoDB.CreateMoonAsync(moonDTO);
                var ephemerisDTOs = new List<EphemerisMongoDTO>();

               foreach (Ephemeris ephemeris in moon.Ephemeris) 
               {
                    ephemerisDTOs.Add(EphemerisMongoDTO.ConvertToEphemerisMongoDTO(ephemeris, moon.HorizonId, moonDTO.Id));
               }
               await _ephemerisRepoMongoDB.CreateMoonEphemerisManyAsync(ephemerisDTOs);
            }
        }

        public async Task MigrateArtificialSatellites() 
        {
            var artificialSatellitesDTOs = new List<ArtificialSatelliteMongoDTO>();
            List<ArtificialSatellite> artificialSatellites = await _artificialSatelliteRepoMySQL.RequestAllSatellites();

            foreach (var artificialSatellite in artificialSatellites) 
            {
                artificialSatellitesDTOs.Add(ArtificialSatelliteMongoDTO.ConvertToArtificialSatelliteMongoDTO(artificialSatellite));
            }
            await _artificialSatelliteRepoMongoDB.CreateSatellites(artificialSatellitesDTOs);
        }   
    }
}