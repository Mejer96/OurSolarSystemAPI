using OurSolarSystemAPI.Repository.MySQL;
using OurSolarSystemAPI.Repository.NEO4J;
using OurSolarSystemAPI.Models;
namespace OurSolarSystemAPI.Service.NEO4J 
{
    public class MigrationServiceNEO4J 
    {
        private readonly BarycenterRepositoryMySQL _barycenterRepoMySQL;
        private readonly PlanetRepositoryMySQL _planetRepoMySQL;
        private readonly MoonRepositoryMySQL _moonRepoMySQL;
        private readonly ArtificialSatelliteRepositoryMySQL _artificialSatelliteRepoMySQL;
        private readonly BarycenterRepositoryNEO4J _barycenterRepoNEO4J;
        private readonly PlanetRepositoryNEO4J _planetRepoNEO4J;
        private readonly MoonRepositoryNEO4J _moonRepoNEO4J;
        private readonly ArtificialSatelliteRepositoryNEO4J _artificialSatelliteRepoNEO4J;
        private readonly EphemerisRepositoryNEO4J _ephemerisRepoNEO4J;

        public MigrationServiceNEO4J(
            BarycenterRepositoryMySQL barycenterRepoMySQL,
            PlanetRepositoryMySQL planetRepoMySQL,
            MoonRepositoryMySQL moonRepoMySQL,
            ArtificialSatelliteRepositoryMySQL artificialSatelliteRepoMySQL,
            BarycenterRepositoryNEO4J barycenterRepoNEO4J,
            PlanetRepositoryNEO4J planetRepoNEO4J,
            MoonRepositoryNEO4J moonRepoNEO4J,
            ArtificialSatelliteRepositoryNEO4J artificialSatelliteRepoNEO4J,
            EphemerisRepositoryNEO4J ephemerisRepositoryNEO4J
        ) 
        {
            _barycenterRepoMySQL = barycenterRepoMySQL;
            _planetRepoMySQL = planetRepoMySQL;
            _moonRepoMySQL = moonRepoMySQL;
            _artificialSatelliteRepoMySQL = artificialSatelliteRepoMySQL;
            _barycenterRepoNEO4J = barycenterRepoNEO4J;
            _planetRepoNEO4J = planetRepoNEO4J;
            _moonRepoNEO4J = moonRepoNEO4J;
            _artificialSatelliteRepoNEO4J = artificialSatelliteRepoNEO4J;
            _ephemerisRepoNEO4J = ephemerisRepositoryNEO4J;
        }

        public async Task MigrateBarycenters() 
        {
            List<Barycenter> barycenters = await _barycenterRepoMySQL.requestAllBarycentersWithEphemeris();
            await _barycenterRepoNEO4J.CreateSolarSystemBarycenterNode(barycenters[0]);
            barycenters.RemoveAt(0);

            foreach (var barycenter in barycenters) 
            {
                await _barycenterRepoNEO4J.CreateBarycenterNodeFromObject(barycenter);
                await _ephemerisRepoNEO4J.createEphimerisNodesFromList(barycenter.Ephemeris, barycenter.HorizonId, "Barycenter");
            }
        }


        public async Task MigratePlanets() 
        {
            List<Planet> planets = await _planetRepoMySQL.requestAllPlanetsWithEphemeris();

            foreach (var planet in planets) 
            {
                await _planetRepoNEO4J.CreatePlanetNode(planet);
                await _ephemerisRepoNEO4J.createEphimerisNodesFromList(planet.Ephemeris, planet.HorizonId, "Planet");
            }
        }

        public async Task MigrateMoons() 
        {
            List<Moon> moons = await _moonRepoMySQL.requestAllMoonsWithEphemeris();
            var MoonDicts = new List<Dictionary<string, object>>(); 

            foreach (var moon in moons) 
            {
                await _moonRepoNEO4J.createMoonNodeFromMoonObject(moon, moon.PlanetHorizonId, moon.BarycenterHorizonId);
                await _ephemerisRepoNEO4J.createEphimerisNodesFromList(moon.Ephemeris, moon.HorizonId, "Moon");
            }
        }

        public async Task MigrateArtificialSatellites() 
        {
            List<ArtificialSatellite> satellites = await _artificialSatelliteRepoMySQL.RequestAllSatellites();

            foreach (var satelitte in satellites) 
            {
                await _artificialSatelliteRepoNEO4J.createSatelliteNodeFromObject(satelitte, 399);
                await _ephemerisRepoNEO4J.createTLENodesFromList(satelitte.Tle, satelitte.NoradId);
            }
        }
    


    }
}