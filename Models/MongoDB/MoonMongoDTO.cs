using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OurSolarSystemAPI.Models.MongoDB 
{
    public class MoonMongoDTO 
    {
        [BsonId]
        public ObjectId Id {get; set; }
        public int HorizonId { get; set; }
        public List<EphemerisMoon>? Ephemeris { get; set; }
        public required string Name { get; set; }
        public string? MeanRadius { get; set; }
        public string? Density { get; set; }
        public string? Gm { get; set; }
        public string? SemiMajorAxis { get; set; }
        public string? GravitationalParameter { get; set; }
        public string? GeometricAlbedo { get; set; }
        public string? OrbitalPeriod { get; set; }
        public string? Eccentricity { get; set; }
        public string? RotationalPeriod { get; set; }
        public string? Inclination { get; set; }

        public static MoonMongoDTO ConvertToMoonMongoDTO(Moon moon)
        {

            return new MoonMongoDTO
            {
                HorizonId = moon.HorizonId,
                Ephemeris = moon.Ephemeris,
                Name = moon.Name,
                MeanRadius = moon.MeanRadius,
                Density = moon.Density,
                Gm = moon.Gm,
                SemiMajorAxis = moon.SemiMajorAxis,
                GravitationalParameter = moon.GravitationalParameter,
                GeometricAlbedo = moon.GeometricAlbedo,
                OrbitalPeriod = moon.OrbitalPeriod,
                Eccentricity = moon.Eccentricity,
                RotationalPeriod = moon.RotationalPeriod,
                Inclination = moon.Inclination
            };
        }

    }
}