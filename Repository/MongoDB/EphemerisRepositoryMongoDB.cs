using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using OurSolarSystemAPI.Models;
using OurSolarSystemAPI.Models.MongoDB;
using System.Collections.Generic;


namespace OurSolarSystemAPI.Repository.MongoDB 
{
    public class EphemerisRepositoryMongoDB
{
    private readonly IMongoCollection<EphemerisMongoDTO> _ephemerisPlanetCollection;
    private readonly IMongoCollection<EphemerisMongoDTO> _ephemerisBarycenterCollection;
    private readonly IMongoCollection<EphemerisMongoDTO> _ephemerisMoonCollection;

    public EphemerisRepositoryMongoDB(MongoDbContext context)
        {

            _ephemerisPlanetCollection = context.GetCollection<EphemerisMongoDTO>("EphemerisPlanets");
            _ephemerisBarycenterCollection = context.GetCollection<EphemerisMongoDTO>("EphemerisBarycenter");
            _ephemerisMoonCollection = context.GetCollection<EphemerisMongoDTO>("EphemerisMoons");
        }
    
    public async Task CreatePlanetEphemerisAsync(EphemerisMongoDTO ephemeris)
        {
            await _ephemerisPlanetCollection.InsertOneAsync(ephemeris);
        }
    
        public async Task CreatePlanetEphemerisManyAsync(List<EphemerisMongoDTO> ephemeris)
        {
            await _ephemerisPlanetCollection.InsertManyAsync(ephemeris);
        }

        public async Task CreateBarycenterEphemerisManyAsync(List<EphemerisMongoDTO> ephemeris)
        {
            await _ephemerisBarycenterCollection.InsertManyAsync(ephemeris);
        }

        public async Task CreateMoonEphemerisManyAsync(List<EphemerisMongoDTO> ephemeris)
        {
            await _ephemerisMoonCollection.InsertManyAsync(ephemeris);
        }

}

}