using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurSolarSystemAPI.Models
{
    public class Moon
    {
        [BsonIgnore]
        public int Id { get; set; }
        [BsonIgnore]
        public int PlanetId { get; set; }
        [BsonIgnore]
        public Planet Planet { get; set; }
        public int BarycenterId { get; set; }
        public Barycenter Barycenter { get; set; }
        public int BarycenterHorizonId { get; set; }
        [BsonIgnore]
        public int PlanetHorizonId { get; set; }
        [NotMapped]
        [BsonIgnore]
        public string? PlanetName { get; set; }
        public int HorizonId { get; set; }
        [BsonIgnore]
        public List<EphemerisMoon>? Ephemeris { get; set; }
        public required string Name { get; set; }
    }


}
