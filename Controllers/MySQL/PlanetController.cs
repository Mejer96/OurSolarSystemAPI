using Microsoft.AspNetCore.Mvc;
using OurSolarSystemAPI.Service.MySQL;
using OurSolarSystemAPI.Models;

namespace OurSolarSystemAPI.Controllers.MySQL 
{
    [ApiController]
    [Route("api/mysql")]
    public class PlanetController : ControllerBase
    {
        private readonly PlanetServiceMySQL _planetService;

        public PlanetController(PlanetServiceMySQL planetService) 
        {
            _planetService = planetService;
        }

        [HttpGet("get-planet-location-by-name-and-date")]
        public IActionResult RequestPlanetLocationByNameAndDate(string name, DateTime dateTime) 
        {
            _planetService.RequestPlanetLocationByNameAndDateTime(name, dateTime);

            return Ok();
        }

        [HttpGet("get-current-planet-location-by-name")]
        public IActionResult RequestCurrentPlanetLocationByName(string name) 
        {
            _planetService.RequestPlanetLocationByNameAndDateTime(name, DateTime.Now.Date);

            return Ok();
        }

        [HttpGet("get-planet-locations-by-horizon-id")]
        public IActionResult RequestPlanetLocationByHorizonId(int horizonId) 
        {
            Planet planet = _planetService.RequestPlanetLocationsByHorizonId(horizonId);

            return Ok(planet);
        }

        [HttpGet("get-all-planet-locations")]
        public async Task<IActionResult> RequestAllPlanetLocations() 
        {
            List<Planet> planets = await _planetService.requestAllPlanetsWithEphemeris();

            return Ok(planets);
        }

        [HttpGet("get-planet-ephemeris-with-pagination-by-horizon-id")]
        public async Task<IActionResult> RequestPlanetEphemerisWithPagination(int horizonId, int pageNumber, int pageSize) 
        {
            List<EphemerisPlanet> data = await _planetService.RequestPlanetEphemerisWithPagination(horizonId, pageNumber, pageSize);

            return Ok(data);
        }

        
    }

}