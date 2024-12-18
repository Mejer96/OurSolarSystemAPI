using System.Numerics;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OurSolarSystemAPI.Models
{
    public class ArtificialSatellite
    {
        public int Id { get; set; }
        public int PlanetId { get; set; }
        public Planet Planet { get; set; }
        public List<TleArtificialSatellite>? Tle { get; set; }
        public string? LaunchDate { get; set; }
        public string? LaunchSite { get; set; }
        public double? BStarDragTerm { get; set; }
        public double? Eccentricity { get; set; }
        public double? MeanAnomaly { get; set; }
        public int? OrbitNumber { get; set; }
        public string? Source { get; set; }
        [Required]
        public int NoradId { get; set; }
        public string? NssdcId { get; set; }
        public string? Perigee { get; set; }
        public double? Apogee { get; set; }
        public double? Inclination { get; set; }
        public string? Period { get; set; }
        public string? SemiMajorAxis { get; set; }
        public string? Rcs { get; set; }
        public required string Name { get; set; }
    }

}
