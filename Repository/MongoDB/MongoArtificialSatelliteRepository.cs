using MongoDB.Driver;
using OurSolarSystemAPI.Models;

namespace OurSolarSystemAPI.Repository.MongoDB
{
    public class MongoArtificialSatelliteRepository
    {
        private readonly IMongoCollection<ArtificialSatellite> _satellites;

        public MongoArtificialSatelliteRepository(MongoDbContext context)
        {
            _satellites = context.GetCollection<ArtificialSatellite>("ArtificialSatellites");
        }

        // Create a new artificial satellite
        public async Task CreateSatelliteAsync(ArtificialSatellite satellite)
        {
            await _satellites.InsertOneAsync(satellite);
        }

        // Retrieve a satellite by NORAD ID
        public async Task<ArtificialSatellite?> GetSatelliteByNoradIdAsync(int noradId)
        {
            var filter = Builders<ArtificialSatellite>.Filter.Eq(s => s.NoradId, noradId);
            return await _satellites.Find(filter).FirstOrDefaultAsync();
        }

        // Retrieve satellites launched from a specific site
        public async Task<List<ArtificialSatellite>> GetSatellitesByLaunchSiteAsync(string launchSite)
        {
            var filter = Builders<ArtificialSatellite>.Filter.Eq(s => s.LaunchSite, launchSite);
            return await _satellites.Find(filter).ToListAsync();
        }

        // Update an existing satellite
        public async Task UpdateSatelliteAsync(int noradId, ArtificialSatellite updatedSatellite)
        {
            var filter = Builders<ArtificialSatellite>.Filter.Eq(s => s.NoradId, noradId);
            await _satellites.ReplaceOneAsync(filter, updatedSatellite);
        }

        // Delete a satellite by NORAD ID
        public async Task DeleteSatelliteAsync(int noradId)
        {
            var filter = Builders<ArtificialSatellite>.Filter.Eq(s => s.NoradId, noradId);
            await _satellites.DeleteOneAsync(filter);
        }
    }
}
