namespace OurSolarSystemAPI.Models
{
    public class PlanetOverview
    {
        public int PlanetId { get; set; }
        public string PlanetName { get; set; }
        public int MoonCount { get; set; }
        public int SatelliteCount { get; set; }
    }
}
