using HtmlAgilityPack;
using OurSolarSystemAPI.Interfaces;
using OurSolarSystemAPI.Models;
using OurSolarSystemAPI.Repository;

namespace OurSolarSystemAPI.Service 
{
    public class PlanetService 
    {
        private readonly PlanetRepository _planetRepo;

        public PlanetService(PlanetRepository planetRepo) 
        {
             _planetRepo = planetRepo;
        }

        public Planet? RequestPlanetLocationByNameAndDateTime(string name, DateTime dateTime) 
        {
            return _planetRepo.RequestPlanetLocationByNameAndDateTime(name, dateTime);
        }

        public List<PlanetLocation> GetPlanetLocations(DateTime startDate, DateTime endDate)
        {
            // Business logic: validate inputs
            if (startDate >= endDate)
            {
                throw new ArgumentException("Start date must be earlier than end date.");
            }

            // Call repository method
            return _planetRepo.GetPlanetLocations(startDate, endDate);
        }

        public decimal GetTotalPlanetMass()
        {
            return _planetRepo.GetTotalPlanetMass();
        }

        public List<PlanetOverview> GetPlanetOverview()
        {
            return _planetRepo.GetPlanetOverview();
        }


    }

}