using System.Text.Json.Serialization;

namespace OurSolarSystemAPI.Models
{
    public class EphemerisSun : Ephemeris
    {
        public int Id { get; set; }
        public int SunId { get; set; }
        public int SunHorizonId { get; set; }
        [JsonIgnore]
        public Star Sun { get; set; }

        public static EphemerisSun ConvertEphemerisDictToObject(Dictionary<string, object> ephemerisDict, Star sun)
        {
            return new EphemerisSun
            {
                Sun = sun,
                SunHorizonId = sun.HorizonId,
                PositionX = Convert.ToDouble(ephemerisDict["positionX"]),
                PositionY = Convert.ToDouble(ephemerisDict["positionY"]),
                PositionZ = Convert.ToDouble(ephemerisDict["positionZ"]),
                VelocityX = Convert.ToDouble(ephemerisDict["velocityX"]),
                VelocityY = Convert.ToDouble(ephemerisDict["velocityY"]),
                VelocityZ = Convert.ToDouble(ephemerisDict["velocityZ"]),
                DateTime = (DateTime)ephemerisDict["dateTime"],
                JulianDate = Convert.ToDouble(ephemerisDict["julianDate"])
            };
        }
    }

}