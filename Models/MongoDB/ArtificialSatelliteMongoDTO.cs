using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OurSolarSystemAPI.Models.MongoDB
{
    public class ArtificialSatelliteMongoDTO
    {
        [BsonId]
        public ObjectId Id {get; set; }
        public List<TleArtificialSatelliteMongoDTO> Tle { get; set; }
        public string? LaunchDate { get; set; }
        public string? LaunchSite { get; set; }
        public double? BStarDragTerm { get; set; }
        public double? Eccentricity { get; set; }
        public double? MeanAnomaly { get; set; }
        public int? OrbitNumber { get; set; }
        public string? Source { get; set; }
        public int NoradId { get; set; }
        public string? NssdcId { get; set; }
        public string? Perigee { get; set; }
        public double? Apogee { get; set; }
        public double? Inclination { get; set; }
        public string? Period { get; set; }
        public string? SemiMajorAxis { get; set; }
        public string? Rcs { get; set; }
        public required string Name { get; set; }

        public static ArtificialSatelliteMongoDTO ConvertToArtificialSatelliteMongoDTO(ArtificialSatellite satellite)
        {
            var tleDTOs = new List<TleArtificialSatelliteMongoDTO>();

            foreach (var tle in satellite.Tle) 
            {
                tleDTOs.Add(TleArtificialSatelliteMongoDTO.ConvertToDto(tle));
            }

            return new ArtificialSatelliteMongoDTO
            {
                Tle = tleDTOs,
                LaunchDate = satellite.LaunchDate,
                LaunchSite = satellite.LaunchSite,
                BStarDragTerm = satellite.BStarDragTerm,
                Eccentricity = satellite.Eccentricity,
                MeanAnomaly = satellite.MeanAnomaly,
                OrbitNumber = satellite.OrbitNumber,
                Source = satellite.Source,
                NoradId = satellite.NoradId,
                NssdcId = satellite.NssdcId,
                Perigee = satellite.Perigee,
                Apogee = satellite.Apogee,
                Inclination = satellite.Inclination,
                Period = satellite.Period,
                SemiMajorAxis = satellite.SemiMajorAxis,
                Rcs = satellite.Rcs,
                Name = satellite.Name
            };
        }
    }



}