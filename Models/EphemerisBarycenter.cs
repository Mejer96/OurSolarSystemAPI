using Google.Protobuf.WellKnownTypes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace OurSolarSystemAPI.Models
{
    public class EphemerisBarycenter : Ephemeris
    {
        [BsonIgnore]
        public int Id { get; set; }
        [BsonIgnore]
        public int BarycenterId { get; set; }
        public int BarycenterHorizonId { get; set; }
        [BsonIgnore]
        public Barycenter? Barycenter { get; set; } 

        public static EphemerisBarycenter ConvertEphemerisDictToObject(Dictionary<string, object> ephemerisDict, Barycenter barycenter)
        {
            return new EphemerisBarycenter 
            {
                Barycenter = barycenter,
                BarycenterHorizonId = barycenter.HorizonId,
                PositionX = Convert.ToDouble(ephemerisDict["positionX"]),
                PositionY = Convert.ToDouble(ephemerisDict["positionY"]),
                PositionZ = Convert.ToDouble(ephemerisDict["positionZ"]),
                VelocityX = Convert.ToDouble(ephemerisDict["velocityX"]),
                VelocityY = Convert.ToDouble(ephemerisDict["velocityY"]),
                VelocityZ = Convert.ToDouble(ephemerisDict["velocityZ"]),
                DateTime = (DateTime) ephemerisDict["dateTime"],
                JulianDate = Convert.ToDouble(ephemerisDict["julianDate"])
            };
        }
    }
}