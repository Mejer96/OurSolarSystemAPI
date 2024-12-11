using Microsoft.AspNetCore.Mvc;
using OurSolarSystemAPI.Service;

namespace OurSolarSystemAPI.Controllers.MySQL 
{
    [ApiController]
    [Route("api/mysql")]
    public class MoonController : ControllerBase
    {


        private readonly MoonService _moonService;


        public MoonController(MoonService moonService)
        {
            _moonService = moonService;
        }

        [HttpGet("moon-locations")]
        public IActionResult GetMoonLocations([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int planetId)
        {
            try
            {
                var locations = _moonService.GetMoonLocations(startDate, endDate, planetId);
                return Ok(locations);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("with-horizon")]
        public IActionResult GetMoonsWithHorizon()
        {
            try
            {
                var moons = _moonService.GetMoonsWithHorizon();
                return Ok(moons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }

}