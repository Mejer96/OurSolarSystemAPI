
using OurSolarSystemAPI.Models;
using OurSolarSystemAPI.Repository.MySQL;

namespace OurSolarSystemAPI.Service
{
    public class BarycenterService
    {
        private readonly BarycenterRepositoryMySQL _barycenterRepo;

        public BarycenterService(BarycenterRepositoryMySQL barycenterRepo)
        {
            _barycenterRepo = barycenterRepo;
        }

        public Barycenter RequestBarycenterLocationByNameAndDateTime(string name, DateTime dateTime)
        {
            return _barycenterRepo.RequestBarycenterLocationByNameAndDateTime(name, dateTime);
        }

        public Barycenter RequestBarycenterLocationByHorizonIdAndDateTime(int horizonId, DateTime dateTime)
        {
            return _barycenterRepo.RequestBarycenterLocationByHorizonIdAndDateTime(horizonId, dateTime);
        }


    }

}