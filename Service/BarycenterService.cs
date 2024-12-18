using Microsoft.AspNetCore.Http.HttpResults;
using OurSolarSystemAPI.Models;
using OurSolarSystemAPI.Repository;

namespace OurSolarSystemAPI.Service 
{
    public class BarycenterService 
    {
        private readonly BarycenterRepository _barycenterRepo;

        public BarycenterService(BarycenterRepository barycenterRepo) 
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