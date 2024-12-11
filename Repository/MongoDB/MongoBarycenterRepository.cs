using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using OurSolarSystemAPI.Models;

namespace OurSolarSystemAPI.Repository.MongoDB
{
    public class MongoBarycenterRepository
    {
        private readonly IMongoCollection<Barycenter> _barycenters;
        private readonly IMongoCollection<EphemerisBarycenter> _ephemerisBarycenters;

        public MongoBarycenterRepository(MongoDbContext context)
        {
            _barycenters = context.GetCollection<Barycenter>("Barycenters");
            _ephemerisBarycenters = context.GetCollection<EphemerisBarycenter>("EphemerisBarycenters");
        }

        public async Task CreateBarycenterAsync(Barycenter barycenter)
        {
            try
            {
                await _barycenters.InsertOneAsync(barycenter);
            }
            catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
            {
                Console.WriteLine($"Duplicate key error for Id {barycenter.Id}. Skipping insertion.");
            }
        }

        public async Task CreateEphemerisBarycenterAsync(EphemerisBarycenter ephemerisBarycenter)
        {
            await _ephemerisBarycenters.InsertOneAsync(ephemerisBarycenter);
        }


        public async Task<Barycenter?> GetBarycenterByNameAndDateAsync(string name, DateTime dateTime)
        {
            var filter = Builders<Barycenter>.Filter.And(
                Builders<Barycenter>.Filter.Eq(b => b.Name, name),
                Builders<Barycenter>.Filter.ElemMatch(b => b.Ephemeris, e => e.DateTime.Date == dateTime.Date)
            );
            return await _barycenters.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<List<Barycenter>> GetAllBarycentersAsync()
        {
            return await _barycenters.Find(_ => true).ToListAsync();
        }
        public async Task<List<EphemerisBarycenter>> GetAllEphemerisBarycentersAsync()
        {
            return await _ephemerisBarycenters.Find(_ => true).ToListAsync();
        }
    }
}

