using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using OurSolarSystemAPI.Models;

namespace OurSolarSystemAPI.Repository.MongoDB
{
    public class MongoPlanetRepository
    {
        private readonly IMongoCollection<Planet> _planets;
        private readonly IMongoCollection<EphemerisPlanet> _ephemerisPlanets;

        public MongoPlanetRepository(MongoDbContext context)
        {
            _planets = context.GetCollection<Planet>("Planets");
            _ephemerisPlanets = context.GetCollection<EphemerisPlanet>("EphemerisPlanets");
        }

        // Create a new planet
        public async Task CreatePlanetAsync(Planet planet)
        {
            await _planets.InsertOneAsync(planet);
        }

        // Add moons to an existing planet
        public async Task AddMoonsToExistingPlanetAsync(List<Moon> moons, int horizonPlanetId)
        {
            // Filter to find the specific planet by HorizonId
            var filter = Builders<Planet>.Filter.Eq(p => p.HorizonId, horizonPlanetId);

            // Update definition to push moons into the Moons array
            var update = Builders<Planet>.Update.PushEach(p => p.Moons, moons);

            // Perform the update
            await _planets.UpdateOneAsync(filter, update);
        }

        public async Task CreateEphemerisPlanetAsync(EphemerisPlanet ephemerisPlanet)
        {
            var ephemerisCollection = _ephemerisPlanets;
            await ephemerisCollection.InsertOneAsync(ephemerisPlanet);
        }
        public async Task<List<Planet>> GetAllPlanetsAsync()
        {
            return await _planets.Find(_ => true).ToListAsync();
        }
    }
}
