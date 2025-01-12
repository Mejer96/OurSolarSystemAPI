using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace OurSolarSystemAPI.Repository.MongoDB
{
    public class PlanetRepositoryMongoDB
    {
        private readonly IMongoCollection<PlanetMongoDTO> _planets;
        private readonly IMongoCollection<StarMongoDTO> _sun;
        private readonly IMongoCollection<EphemerisMongoDTO> _ephemeris;

        public PlanetRepositoryMongoDB(MongoDbContext context)
        {
            _planets = context.GetCollection<PlanetMongoDTO>("Planets");
            _sun = context.GetCollection<StarMongoDTO>("Sun");
            _ephemeris = context.GetCollection<EphemerisMongoDTO>("EphemerisPlanets");
        }

        public async Task CreatePlanet(PlanetMongoDTO planet)
        {
            await _planets.InsertOneAsync(planet);
        }

        public async Task CreateSun(StarMongoDTO sun)
        {
            await _sun.InsertOneAsync(sun);
        }

        public async Task<PlanetMongoDTO> GetByHorizonId(int horizonId)
        {
            return await _planets.Find(p => p.HorizonId == horizonId).FirstOrDefaultAsync();
        }

        public async Task<PlanetMongoDTO> GetByName(string name)
        {
            return await _planets.Find(p => p.Name == name).FirstOrDefaultAsync();
        }


        public async Task<(EphemerisMongoDTO firstEphemeris, EphemerisMongoDTO secondEphemeris)> GetDistance(int firstHorizonId, int secondHorizonId, DateTime dateTime)
        {
            EphemerisMongoDTO firstEphemeris = await _ephemeris
                .Find(e => e.CelestialBodyHorizonId == firstHorizonId && e.DateTime == dateTime.Date)
                .FirstOrDefaultAsync();

            EphemerisMongoDTO secondEphemeris = await _ephemeris
                .Find(e => e.CelestialBodyHorizonId == secondHorizonId && e.DateTime == dateTime.Date)
                .FirstOrDefaultAsync();

            return (firstEphemeris, secondEphemeris);
        }

        public async Task<EphemerisMongoDTO> GetLocationByHorizonIdAndDate(int horizonId, DateTime dateTime)
        {

            var ephemeris = await _ephemeris
                .Find(e => e.CelestialBodyHorizonId == horizonId && e.DateTime == dateTime)
                .FirstOrDefaultAsync();

            return ephemeris;
        }

        public async Task<List<EphemerisMongoDTO>> GetLocationsByHorizonId(int horizonId)
        {
            var ephemeris = await _ephemeris
                .Find(e => e.CelestialBodyHorizonId == horizonId)
                .ToListAsync();

            return ephemeris;
        }

        public async Task<List<MoonMongoDTO>> GetMoonsByHorizonId(int horizonID)
        {
            var moons = await _planets
                .Find(p => p.HorizonId == horizonID)
                .Project(p => p.Moons)
                .FirstOrDefaultAsync();

            return moons;
        }

    }
}
