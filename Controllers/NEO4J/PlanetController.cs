using Microsoft.AspNetCore.Mvc;
using OurSolarSystemAPI.Service.MySQL;
using OurSolarSystemAPI.Repository.NEO4J;
using OurSolarSystemAPI.Models;

namespace OurSolarSystemAPI.Controllers.NEO4J 
{
    [ApiController]
    [Route("api/neo4j/planet")]
    public class PlanetController : ControllerBase
    {
    
        private readonly PlanetRepositoryNEO4J _planetRepo;

        public PlanetController(PlanetRepositoryNEO4J planetRepo) 
        {
            _planetRepo = planetRepo;
        }

        [HttpGet("get-planet-location-by-horizon-id-and-date")]
        public async Task<IActionResult> RequestPlanetLocationByNameAndDate(int horizonId, DateTime date) 
        {
            await _planetRepo.FetchEphemerisByHorizonIdAndDate(horizonId, date);

            return Ok();
        }

        [HttpGet("get-all-planet-locations-by-horizon-id")]
        public async Task<IActionResult> RequestPlanetLocationsByHorizonId(int horizonId) 
        {
            string json = await _planetRepo.FetchAllEphemerisByHorizonId(horizonId);

            return Ok(json);
        }

        // [HttpGet("get-current-planet-location-by-name")]
        // public async Task<IActionResult> RequestCurrentPlanetLocationByName(string name) 
        // {
        //     await _planetService.RequestPlanetLocationByNameAndDateTime(name, DateTime.Now.Date);

        //     return Ok();
        // }


        
    }

}