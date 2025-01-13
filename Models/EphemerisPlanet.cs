using System.Text.Json.Serialization;

namespace OurSolarSystemAPI.Models
{
    public class EphemerisPlanet : Ephemeris
    {
        public int Id { get; set; }
        public int PlanetId { get; set; }
        public int PlanetHorizonId { get; set; }
        [JsonIgnore]
        public Planet Planet { get; set; }

        public static EphemerisPlanet ConvertEphemerisDictToObject(Dictionary<string, object> ephemerisDict, Planet planet)
        {
            return new EphemerisPlanet
            {
                Planet = planet,
                PlanetHorizonId = planet.HorizonId,
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
