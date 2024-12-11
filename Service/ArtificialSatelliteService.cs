using OurSolarSystemAPI.Models;
using OurSolarSystemAPI.Repository;

namespace OurSolarSystemAPI.Service 
{
    public class ArtificialSatelliteService 
    {
        private readonly ArtificialSatelliteRepository _artificialSatelliteRepo;

        public ArtificialSatelliteService(ArtificialSatelliteRepository artificialSatelliteRepo) 
        {
             _artificialSatelliteRepo = artificialSatelliteRepo;
        }

        public ArtificialSatellite RequestSatelliteByNoradId(int noradId) 
        {
           return _artificialSatelliteRepo.RequestSatelitteByNoradId(noradId);
        }

        public ArtificialSatellite RequestSatelliteByNoradIdAndDateTime(int noradId, DateTime dateTime) 
        {
            return _artificialSatelliteRepo.RequestSatelliteLocationByNoradIdAndDateTime(noradId, dateTime);
        }

        public void LogSatelliteSearch(int noradId)
        {
            _artificialSatelliteRepo.LogSatelliteSearch(noradId);
        }

        public int GetSumOfSatelliteOrbits()
        {
            return _artificialSatelliteRepo.GetSumOfSatelliteOrbits();
        }


    }

}