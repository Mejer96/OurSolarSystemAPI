
using OurSolarSystemAPI.Models;
using OurSolarSystemAPI.Repository;
namespace OurSolarSystemAPI.Service 
{
    public class MoonService 
    {
            private readonly MoonRepository _moonRepo;

        public MoonService(MoonRepository moonRepo) 
        {
             _moonRepo = moonRepo;
        }

        public List<MoonLocation> GetMoonLocations(DateTime startDate, DateTime endDate, int planetId)
        {
            if (startDate >= endDate)
            {
                throw new ArgumentException("Start date must be earlier than end date.");
            }

            return _moonRepo.GetMoonLocations(startDate, endDate, planetId);
        }

        public List<MoonsWithHorizon> GetMoonsWithHorizon()
        {
            return _moonRepo.GetMoonsWithHorizon();
        }


    }

}