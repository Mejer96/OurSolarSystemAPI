using OurSolarSystemAPI.Models;

namespace OurSolarSystemAPI.Interfaces
{
    public interface IplanetRepository
    {
        List<PlanetLocation> GetPlanetLocations(DateTime startDate, DateTime endDate);

    }
}
