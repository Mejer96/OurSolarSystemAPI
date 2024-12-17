using MongoDB.Driver;
using OurSolarSystemAPI.Models;

namespace OurSolarSystemAPI.Repository.MongoDB
{
    public class ArtificialSatelliteRepositoryMongoDB
    {
        private readonly IMongoCollection<ArtificialSatellite> _satellites;

        public ArtificialSatelliteRepositoryMongoDB(MongoDbContext context)
        {
            _satellites = context.GetCollection<ArtificialSatellite>("ArtificialSatellites");
        }

        public async Task CreateSatellite(ArtificialSatellite satellite)
        {
            await _satellites.InsertOneAsync(satellite);
        }

        public async Task CreateSatellites(List<ArtificialSatellite> satellites)
        {
            await _satellites.InsertManyAsync(satellites);
        }

        public async Task<ArtificialSatellite?> GetSatelliteByNoradId(int noradId)
        {
            var filter = Builders<ArtificialSatellite>.Filter.Eq(s => s.NoradId, noradId);
            return await _satellites.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<ArtificialSatellite>> GetSatellitesByLaunchSite(string launchSite)
        {
            var filter = Builders<ArtificialSatellite>.Filter.Eq(s => s.LaunchSite, launchSite);
            return await _satellites.Find(filter).ToListAsync();
        }

        public async Task UpdateSatellite(int noradId, ArtificialSatellite updatedSatellite)
        {
            var filter = Builders<ArtificialSatellite>.Filter.Eq(s => s.NoradId, noradId);
            await _satellites.ReplaceOneAsync(filter, updatedSatellite);
        }

        public async Task DeleteSatellite(int noradId)
        {
            var filter = Builders<ArtificialSatellite>.Filter.Eq(s => s.NoradId, noradId);
            await _satellites.DeleteOneAsync(filter);
        }
    }
}
