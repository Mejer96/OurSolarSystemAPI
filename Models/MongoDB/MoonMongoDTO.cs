using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OurSolarSystemAPI.Models.MongoDB 
{
    public class MoonMongoDTO 
    {
        public int HorizonId { get; set; }
        public required string Name { get; set; }

        public static MoonMongoDTO ConvertToMoonMongoDTO(Moon moon)
        {

            return new MoonMongoDTO
            {
                HorizonId = moon.HorizonId,
                Name = moon.Name,
            };
        }

    }
    public class MoonMongoDTOWithId : MoonMongoDTO
    {
        [BsonId]
        public ObjectId Id {get; set; }

        public static new MoonMongoDTOWithId ConvertToMoonMongoDTO(Moon moon)
        {
            return new MoonMongoDTOWithId
            {
                HorizonId = moon.HorizonId,
                Name = moon.Name,
            };
        }
    }


    
}