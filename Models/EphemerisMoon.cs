using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace OurSolarSystemAPI.Models
{
    public class EphemerisMoon : Ephemeris
    {
        [BsonIgnore]
        public int Id { get; set; }
        [BsonIgnore]
        public int MoonId { get; set; }
        [JsonIgnore]
        [BsonIgnore]
        public Moon Moon { get; set; }


        public static EphemerisMoon ConvertEphemerisDictToObject(Dictionary<string, object> ephemerisDict, Moon moon)
        {
            return new EphemerisMoon 
            {
                Moon = moon,
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