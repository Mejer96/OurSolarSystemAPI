using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurSolarSystemAPI.Models
{
    public class EphemerisPlanet : Ephemeris
    {
        [BsonIgnore]
        public int Id { get; set; }
        [BsonIgnore]
        public int PlanetId { get; set; }
        [JsonIgnore]
        [BsonIgnore]
        public Planet Planet { get; set; }

        public static EphemerisPlanet ConvertEphemerisDictToObject(Dictionary<string, object> ephemerisDict, Planet planet)
        {
            return new EphemerisPlanet 
            {
                Planet = planet,
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
