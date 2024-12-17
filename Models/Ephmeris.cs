using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;


namespace OurSolarSystemAPI.Models
{
    public abstract class Ephemeris
    {
        [NotMapped]
        public int HorizonId { get; set;}
        public required double PositionX { get; set; }
        public required double PositionY { get; set; }
        public required double PositionZ { get; set; }
        public required double VelocityX { get; set; }
        public required double VelocityY { get; set; }
        public required double VelocityZ { get; set; }
        public required double JulianDate { get; set; }
        public required DateTime DateTime { get; set; }
        [NotMapped]
        [BsonIgnore]
        public double ScaledPositionX { get; set; }
        [NotMapped]
        [BsonIgnore]
        public double ScaledPositionY { get; set; }
        [NotMapped]
        [BsonIgnore]
        public double ScaledPositionZ { get; set; }

        public Dictionary<string, object> ConvertObjectToDictNEO4J() 
        {
            return new Dictionary<string, object> 
            {
                {"horizonId", HorizonId},
                {"positionX", PositionX},
                {"positionY", PositionY},
                {"positionZ", PositionZ},
                {"velocityX", VelocityX},
                {"velocityY", VelocityY},
                {"velocityZ", VelocityZ},
                {"DateTime", DateTime},
                {"JulianDate", JulianDate}
            };
        }
    }

}