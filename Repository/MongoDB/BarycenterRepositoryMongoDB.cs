using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using OurSolarSystemAPI.Models;

namespace OurSolarSystemAPI.Repository.MongoDB
{
    public class BarycenterRepositoryMongoDB
    {
        private readonly IMongoCollection<BarycenterMongoDTO> _barycenters;
        private readonly IMongoCollection<SolarSystemBarycenterMongoDTO> _solarSystemBarycenter;
        private readonly IMongoCollection<EphemerisBarycenter> _ephemerisBarycenters;

        public BarycenterRepositoryMongoDB(MongoDbContext context)
        {
            _barycenters = context.GetCollection<BarycenterMongoDTO>("Barycenters");
            _ephemerisBarycenters = context.GetCollection<EphemerisBarycenter>("EphemerisBarycenters");
            _solarSystemBarycenter = context.GetCollection<SolarSystemBarycenterMongoDTO>("SolarSystemBarycenter");
        }

        public async Task createSolarSystemBarycenter(SolarSystemBarycenterMongoDTO solarSystemBarycenter)
        {
            await _solarSystemBarycenter.InsertOneAsync(solarSystemBarycenter);
        }

        public async Task CreateBarycenterAsync(BarycenterMongoDTO barycenter)
        {
            await _barycenters.InsertOneAsync(barycenter);
        }



        public async Task CreateEphemerisBarycenterAsync(EphemerisBarycenter ephemerisBarycenter)
        {
            await _ephemerisBarycenters.InsertOneAsync(ephemerisBarycenter);
        }


        // public async Task<BarycenterMongoDTO> GetBarycenterByNameAndDateAsync(string name, DateTime dateTime)
        // {
        //     var filter = Builders<Barycenter>.Filter.And(
        //         Builders<Barycenter>.Filter.Eq(b => b.Name, name),
        //         Builders<Barycenter>.Filter.ElemMatch(b => b.Ephemeris, e => e.DateTime.Date == dateTime.Date)
        //     );
        //     return await _barycenters.Find(filter).FirstOrDefaultAsync();
        // }
        public async Task<List<BarycenterMongoDTO>> GetAllBarycentersAsync()
        {
            return await _barycenters.Find(_ => true).ToListAsync();
        }
        public async Task<List<EphemerisBarycenter>> GetAllEphemerisBarycentersAsync()
        {
            return await _ephemerisBarycenters.Find(_ => true).ToListAsync();
        }
    }
}

