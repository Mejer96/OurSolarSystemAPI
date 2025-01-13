using System.ComponentModel.DataAnnotations.Schema;


namespace OurSolarSystemAPI.Models
{
    public abstract class Ephemeris
    {
        public int Id { get; set; }
        [NotMapped]
        public int HorizonId { get; set; }
        public required double PositionX { get; set; }
        public required double PositionY { get; set; }
        public required double PositionZ { get; set; }
        public required double VelocityX { get; set; }
        public required double VelocityY { get; set; }
        public required double VelocityZ { get; set; }
        public required double JulianDate { get; set; }
        public required DateTime DateTime { get; set; }
        [NotMapped]
        public double ScaledPositionX { get; set; }
        [NotMapped]
        public double ScaledPositionY { get; set; }
        [NotMapped]
        public double ScaledPositionZ { get; set; }
        [NotMapped]
        public double ScaledVelocityX { get; set; }
        [NotMapped]
        public double ScaledVelocityY { get; set; }
        [NotMapped]
        public double ScaledVelocityZ { get; set; }

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