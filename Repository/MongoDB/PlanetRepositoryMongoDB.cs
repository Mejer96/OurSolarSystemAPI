using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using OurSolarSystemAPI.Models.MongoDB;
using OurSolarSystemAPI.Models;

namespace OurSolarSystemAPI.Repository.MongoDB
{
    public class PlanetRepositoryMongoDB
    {
        private readonly IMongoCollection<PlanetMongoDTO> _planets;

        public PlanetRepositoryMongoDB(MongoDbContext context)
        {
            _planets = context.GetCollection<PlanetMongoDTO>("Planets");
        }

        public async Task CreatePlanetAsync(PlanetMongoDTO planet)
        {
            await _planets.InsertOneAsync(planet);
        }

        public async Task<List<PlanetMongoDTO>> GetAllPlanetsAsync()
        {
            return await _planets.Find(_ => true).ToListAsync();
        }
    }
}
