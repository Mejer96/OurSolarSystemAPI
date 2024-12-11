using MongoDB.Driver;
using OurSolarSystemAPI.Models;

namespace OurSolarSystemAPI.Repository.MongoDB
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var settings = configuration.GetSection("MongoDbSettings");
            var client = new MongoClient(settings["ConnectionString"]);
            _database = client.GetDatabase(settings["DatabaseName"]);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }

        public IMongoCollection<EphemerisBarycenter> GetEphemerisBarycenterCollection() =>
    _database.GetCollection<EphemerisBarycenter>("EphemerisBarycenters");

        public IMongoCollection<EphemerisPlanet> GetEphemerisPlanetCollection() =>
            _database.GetCollection<EphemerisPlanet>("EphemerisPlanets");
    }
}
