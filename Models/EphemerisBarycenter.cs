using Google.Protobuf.WellKnownTypes;

namespace OurSolarSystemAPI.Models
{
    public class EphemerisBarycenter
    {
        public int Id { get; set; }
        public int BarycenterId { get; set; }
        public Barycenter? Barycenter { get; set; } 
        public required double PositionX { get; set; }
        public required double PositionZ { get; set; }
        public required double PositionY { get; set; }
        public required double VelocityX { get; set; }
        public required double VelocityY { get; set; }
        public required double VelocityZ { get; set; }
        public DateTime DateTime { get; set; }

        public double JulianDate { get; set; }

        public static EphemerisBarycenter convertEphemerisDictToObject(Dictionary<string, object> ephemerisDict, Barycenter barycenter)
        {
            return new EphemerisBarycenter 
            {
                Barycenter = barycenter,
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