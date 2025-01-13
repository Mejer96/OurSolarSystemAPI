using OurSolarSystemAPI.Models;


namespace OurSolarSystemAPI.Repository.MongoDB
{
    public class TleArtificialSatelliteMongoDTO()
    {

        public int NoradId { get; set; }
        public string TleFirstLine { get; set; }
        public string TleSecondLine { get; set; }
        public DateTime ScrapedAt { get; set; }

        public static TleArtificialSatelliteMongoDTO ConvertToDto(TleArtificialSatellite satellite)
        {
            return new TleArtificialSatelliteMongoDTO
            {
                NoradId = satellite.NoradId,
                TleFirstLine = satellite.TleFirstLine,
                TleSecondLine = satellite.TleSecondLine,
                ScrapedAt = satellite.ScrapedAt
            };
        }
    }



}