using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OurSolarSystemAPI.Models.MongoDB;

namespace OurSolarSystemAPI.Models
{
    public class SolarSystemBarycenterMongoDTO()
{
        [BsonId]
        public ObjectId Id { get; set; }
        public int HorizonId = 0;
        public string Name = "Solar System Barycenter";
        public required List<BarycenterMongoDTO> barycenters  { get; set; }

    }
}