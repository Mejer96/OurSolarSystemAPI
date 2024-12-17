using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OurSolarSystemAPI.Models
{
    public class SolarSystemBarycenter()
{
    
        public int Id { get; set; }
        public required int HorizonId = 0;
        public required string Name = "Solar System Barycenter";
        public List<Barycenter>? barycenters  { get; set; }

    }
}