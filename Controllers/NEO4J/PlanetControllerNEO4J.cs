using Microsoft.AspNetCore.Mvc;
using OurSolarSystemAPI.Service.NEO4J;


namespace OurSolarSystemAPI.Controllers.NEO4J 
{
    [ApiController]
    [Route("neo4j/api/planet")]
    public class PlanetControllerNEO4J : ControllerBase
    {
    
        private readonly PlanetServiceNEO4J _planetService;

        public PlanetControllerNEO4J(PlanetServiceNEO4J planetRepo) 
        {
            _planetService = planetRepo;
        }

        [HttpGet("get-location-by-horizon-id-and-date")]
        public async Task<IActionResult> RequestLocationByNameAndDate(int horizonId, int month, int day, int year) 
        {

            string json = await _planetService.GetLocationByHorizonIdAndDate(horizonId, new DateTime(year, month, day));

            return Ok(json);
        }

        [HttpGet("get-by-name")]
        public async Task<IActionResult> RequestByName(string name) 
        {
            string json = await _planetService.GetByName(name);

            return Ok(json);
        }

        [HttpGet("get-by-horizon-id")]
        public async Task<IActionResult> RequestByHorizonId(int horizonId) 
        {
            string json = await _planetService.GetByHorizonId(horizonId);

            return Ok(json);
        }

        [HttpGet("get-distance-between")]
        public async Task<IActionResult> RequestDistanceBetween(int firstHorizonId, int secondHorizonId, int month, int day, int year) 
        {

            string json = await _planetService.GetDistance(firstHorizonId, secondHorizonId, new DateTime(year, month, day));

            return Ok(json);
        }


        [HttpGet("get-all-planet-locations-by-horizon-id")]
        public async Task<IActionResult> RequestLocationsByHorizonId(int horizonId) 
        {
            string json = await _planetService.GetLocationsByHorizonId(horizonId);

            return Ok(json);
        }
    }

}