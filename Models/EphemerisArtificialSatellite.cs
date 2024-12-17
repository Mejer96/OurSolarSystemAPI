using MongoDB.Bson.Serialization.Attributes;
using SGPdotNET.Observation;
using System.Text.Json.Serialization;
namespace OurSolarSystemAPI.Models
{
    public class EphemerisArtificialSatellite : Ephemeris
    {
        [BsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public int TleArtificialSatelliteId { get; set; }
        [JsonIgnore]
        public TleArtificialSatellite TleArtificialSatellite { get; set; }

        public static EphemerisArtificialSatellite convertEphemerisDictToObject(Dictionary<string, object> ephemerisDict, TleArtificialSatellite tle)
        {
            return new EphemerisArtificialSatellite 
            {
                TleArtificialSatellite = tle,
                PositionX = (double) ephemerisDict["positionX"],
                PositionY = (double) ephemerisDict["positionY"],
                PositionZ = (double) ephemerisDict["positionZ"],
                VelocityX = (double) ephemerisDict["velocityX"],
                VelocityY = (double) ephemerisDict["velocityY"],
                VelocityZ = (double) ephemerisDict["velocityZ"],
                DateTime = (DateTime) ephemerisDict["dateTime"],
                JulianDate = Convert.ToDouble(ephemerisDict["julianDate"])
            };
        }

    }
}