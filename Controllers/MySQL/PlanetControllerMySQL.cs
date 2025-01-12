using Microsoft.AspNetCore.Mvc;
using OurSolarSystemAPI.Models;
using OurSolarSystemAPI.Repository.MySQL;
using OurSolarSystemAPI.Service.MySQL;

namespace OurSolarSystemAPI.Controllers.MySQL
{
    [ApiController]
    [Route("mysql/api/planet")]
    public class PlanetControllerMySQL : ControllerBase
    {
        private readonly PlanetServiceMySQL _planetService;

        public PlanetControllerMySQL(PlanetServiceMySQL planetService)
        {
            _planetService = planetService;
        }

        [HttpGet("get-by-horizon-id")]
        public async Task<IActionResult> RequestByHorizonId(int horizonId)
        {
            Planet planet = await _planetService.GetByHorizonId(horizonId);

            return Ok(planet);
        }

        [HttpGet("get-location-by-horizon-id-and-date")]
        public async Task<IActionResult> RequestLocationByHorizonIdAndDate(int horizonId, int day, int month, int year)
        {
            Planet planet = await _planetService.GetLocationByHorizonIdAndDate(horizonId, new DateTime(year, month, day));

            return Ok(planet);
        }

        [HttpGet("get-current-location-by-horizon-id")]
        public async Task<IActionResult> RequestCurrentLocation(int horizonId)
        {
            Planet planet = await _planetService.GetLocationByHorizonIdAndDate(horizonId, DateTime.Now.Date);

            return Ok(planet);
        }

        [HttpGet("get-distance-between")]
        public async Task<IActionResult> RequestDistanceBetween(int firstHorizonId, int secondHorizonId, int day, int month, int year)
        {
            DistanceResult result = await _planetService.GetDistance(firstHorizonId, secondHorizonId, new DateTime(year, month, day));

            return Ok(result);
        }


        [HttpGet("get-locations-by-horizon-id")]
        public async Task<IActionResult> RequestLocationsByHorizonId(int horizonId)
        {
            Planet planet = await _planetService.GetLocationsByHorizonId(horizonId);

            return Ok(planet);
        }

        [HttpGet("get-by-name")]
        public async Task<IActionResult> RequestByName(string name)
        {
            Planet planet = await _planetService.GetByName(name);

            return Ok(planet);
        }


        [HttpGet("get-locations-with-pagination-by-horizon-id")]
        public async Task<IActionResult> RequestPlanetEphemerisWithPagination(int horizonId, int pageNumber, int pageSize)
        {
            List<EphemerisPlanet> data = await _planetService.GetEphemerisWithPagination(horizonId, pageNumber, pageSize);

            return Ok(data);
        }

    }

}