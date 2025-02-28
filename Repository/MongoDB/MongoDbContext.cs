using MongoDB.Driver;
using OurSolarSystemAPI.Models;

namespace OurSolarSystemAPI.Repository.MongoDB
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext()
        {
        
            var client = new MongoClient("mongodb://localhost:27017/");
            _database = client.GetDatabase("OurSolarSystem");
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }

        public IMongoCollection<EphemerisBarycenter> GetEphemerisBarycenterCollection() =>
        _database.GetCollection<EphemerisBarycenter>("EphemerisBarycenters");

        public IMongoCollection<UserDTOMongoDB> GetUserCollection() =>
        _database.GetCollection<UserDTOMongoDB>("Users");

        public IMongoCollection<MoonMongoDTOWithId> GetMoonCollection() =>
        _database.GetCollection<MoonMongoDTOWithId>("Moons");

        public IMongoCollection<SolarSystemBarycenterMongoDTO> GetSolarSystemBarycenterCollection() =>
        _database.GetCollection<SolarSystemBarycenterMongoDTO>("SolarSystemBarycenters");

        public IMongoCollection<BarycenterMongoDTO> GetBarycenterCollection() =>
        _database.GetCollection<BarycenterMongoDTO>("Barycenters");

        public IMongoCollection<EphemerisPlanet> GetEphemerisPlanetCollection() =>
        _database.GetCollection<EphemerisPlanet>("EphemerisPlanets");
    }
}
