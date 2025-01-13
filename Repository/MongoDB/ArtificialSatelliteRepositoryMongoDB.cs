using MongoDB.Driver;


namespace OurSolarSystemAPI.Repository.MongoDB
{
    public class ArtificialSatelliteRepositoryMongoDB
    {
        private readonly IMongoCollection<ArtificialSatelliteMongoDTO> _satellites;

        public ArtificialSatelliteRepositoryMongoDB(MongoDbContext context)
        {
            _satellites = context.GetCollection<ArtificialSatelliteMongoDTO>("ArtificialSatellites");
        }

        public async Task CreateSatellite(ArtificialSatelliteMongoDTO satellite)
        {
            await _satellites.InsertOneAsync(satellite);
        }

        public async Task CreateSatellites(List<ArtificialSatelliteMongoDTO> satellites)
        {
            await _satellites.InsertManyAsync(satellites);
        }

        public async Task<ArtificialSatelliteMongoDTO?> GetSatelliteByNoradId(int noradId)
        {
            var filter = Builders<ArtificialSatelliteMongoDTO>.Filter.Eq(s => s.NoradId, noradId);
            return await _satellites.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<ArtificialSatelliteMongoDTO>> GetSatellitesByLaunchSite(string launchSite)
        {
            var filter = Builders<ArtificialSatelliteMongoDTO>.Filter.Eq(s => s.LaunchSite, launchSite);
            return await _satellites.Find(filter).ToListAsync();
        }

        public async Task UpdateSatellite(int noradId, ArtificialSatelliteMongoDTO updatedSatellite)
        {
            var filter = Builders<ArtificialSatelliteMongoDTO>.Filter.Eq(s => s.NoradId, noradId);
            await _satellites.ReplaceOneAsync(filter, updatedSatellite);
        }

        public async Task DeleteSatellite(int noradId)
        {
            var filter = Builders<ArtificialSatelliteMongoDTO>.Filter.Eq(s => s.NoradId, noradId);
            await _satellites.DeleteOneAsync(filter);
        }
    }
}
