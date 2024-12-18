using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using OurSolarSystemAPI.Models;
using OurSolarSystemAPI.Models.MongoDB;

namespace OurSolarSystemAPI.Repository.MongoDB
{
    public class PlanetRepositoryMongoDB
    {
        private readonly IMongoCollection<PlanetMongoDTO> _planets;
        private readonly IMongoCollection<StarMongoDTO> _sun;

        public PlanetRepositoryMongoDB(MongoDbContext context)
        {
            _planets = context.GetCollection<PlanetMongoDTO>("Planets");
            _sun = context.GetCollection<StarMongoDTO>("Sun");
        }

        public async Task CreatePlanetAsync(PlanetMongoDTO planet)
        {
            await _planets.InsertOneAsync(planet);
        }

        public async Task CreateSunAsync(StarMongoDTO sun)
        {
            await _sun.InsertOneAsync(sun);
        }

        public async Task<PlanetMongoDTO> GetPlanetByHorizonIDAsync(int horizonID)
        {
            return await _planets.Find(p => p.HorizonId == horizonID).FirstOrDefaultAsync();
        }

        public async Task<List<PlanetMongoDTO>> GetAllPlanetsAsync()
        {
            return await _planets.Find(_ => true).ToListAsync();
        }

        public async Task<List<PlanetMongoDTO>> GetMoonPlanetsAsync()
        {
            return await _planets.Find(_ => true).ToListAsync();
        }


    }
}
