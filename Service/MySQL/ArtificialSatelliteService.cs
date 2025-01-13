using OurSolarSystemAPI.Models;
using OurSolarSystemAPI.Repository.MySQL;

namespace OurSolarSystemAPI.Service
{
    public class ArtificialSatelliteService
    {
        private readonly ArtificialSatelliteRepositoryMySQL _artificialSatelliteRepo;

        public ArtificialSatelliteService(ArtificialSatelliteRepositoryMySQL artificialSatelliteRepo)
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

        public async Task<bool> CheckIfSatelliteExistsByNoradId(int noradId)
        {
            return await _artificialSatelliteRepo.CheckIfSatelliteExistsByNoradId(noradId);
        }
        /*         public async Task<bool> AddTleToExistingSatellite(int noradId) 
                {

                } */

    }

}