namespace OurSolarSystemAPI.Models
{
    public class SolarSystemBarycenter()
    {

        public int Id { get; set; }
        public int HorizonId { get; set; }
        public Star Sun { get; set; }
        public string Name { get; set; }
        public List<Barycenter>? Barycenters { get; set; }

    }
}