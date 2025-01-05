using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OurSolarSystemAPI.Repository.MongoDB 
{
    public class UserDTOMongoDB
    {
        public string Password;
        [BsonId]
        public ObjectId Id { get; set; } 
        public required string Username { get; set; }
        public string PasswordSalt { get; set; }
    }
}