using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OurSolarSystemAPI.Models.MongoDB
{
    public class BarycenterMongoDTO()
    {
        [BsonId]
        public ObjectId Id {get; set; }
        public required int HorizonId { get; set; }
        public required string Name { get; set; }


        public static BarycenterMongoDTO ConvertToMongoDTO(Barycenter barycenter)
        {
            return new BarycenterMongoDTO
            {
                    HorizonId = barycenter.HorizonId,
                    Name = barycenter.Name
            };
        }
    }
}
    