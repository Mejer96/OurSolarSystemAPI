﻿using MongoDB.Driver;
using OurSolarSystemAPI.Models;
using OurSolarSystemAPI.Models.MongoDB;

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
