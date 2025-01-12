using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OurSolarSystemAPI.Models;

namespace OurSolarSystemAPI.Repository.MongoDB
{
    public class EphemerisMongoDTO
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public ObjectId CelestialBodyId { get; set; }
        public int CelestialBodyHorizonId { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public double PositionZ { get; set; }
        public double VelocityX { get; set; }
        public double VelocityY { get; set; }
        public double VelocityZ { get; set; }
        public DateTime DateTime { get; set; }
        public double JulianDate { get; set; }

        public static EphemerisMongoDTO ConvertToEphemerisMongoDTO(Ephemeris ephemeris, int celestialBodyHorizonId, ObjectId celestialBodyId)
        {
            return new EphemerisMongoDTO
            {
                CelestialBodyHorizonId = celestialBodyHorizonId,
                CelestialBodyId = celestialBodyId,
                PositionX = ephemeris.PositionX,
                PositionY = ephemeris.PositionY,
                PositionZ = ephemeris.PositionZ,
                VelocityX = ephemeris.VelocityX,
                VelocityY = ephemeris.VelocityY,
                VelocityZ = ephemeris.VelocityZ,
                DateTime = ephemeris.DateTime,
                JulianDate = ephemeris.JulianDate
            };
        }
    }



}