using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using OurSolarSystemAPI.Models.MongoDB;

namespace OurSolarSystemAPI.Repository.MongoDB
{
    public class MoonRepositoryMongoDB
    {
        private readonly IMongoCollection<MoonMongoDTOWithId> _moons;

        public MoonRepositoryMongoDB(MongoDbContext context)
        {
            _moons = context.GetCollection<MoonMongoDTOWithId>("Moons");
        }

        public async Task CreateMoonAsync(MoonMongoDTOWithId moon)
        {
            await _moons.InsertOneAsync(moon);
        }


    }
}