using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OurSolarSystemAPI.Models
{
    public class Barycenter()
    {
    
        public int Id { get; set; }
        [Required]
        public required int HorizonId { get; set; }
        [Required]
        public required string Name { get; set; }
        public int SolarSystemBarycenterId { get; set; }
        [JsonIgnore]
        public SolarSystemBarycenter SolarSystemBarycenter { get; set; }
        public List<Planet> Planets { get; set; }
        public List<Moon> Moons { get; set;}
        public List<EphemerisBarycenter> Ephemeris { get; set; }
    }
}
