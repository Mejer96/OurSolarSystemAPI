using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace OurSolarSystemAPI.Models
{
    public class TleArtificialSatellite() 
    {
        [BsonIgnore]
        public int Id { get; set; }
        [BsonIgnore]
        public int ArtificialSatelliteId { get; set; }
        public int NoradId { get; set; }
        [JsonIgnore]
        [BsonIgnore]
        public ArtificialSatellite ArtificialSatellite { get; set; }
        public string TleFirstLine { get; set; }
        public string TleSecondLine { get; set; }
        public DateTime ScrapedAt { get; set; } 

        public bool IsArchived { get; set; }
    }

}