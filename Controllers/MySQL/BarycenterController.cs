using Microsoft.AspNetCore.Mvc;
using OurSolarSystemAPI.Service;

namespace OurSolarSystemAPI.Controllers.MySQL 
{
    [ApiController]
    [Route("api/mysql")]
    public class BarycenterController : ControllerBase
    {
        private readonly BarycenterService _barycenterService;
      

        public BarycenterController(BarycenterService barycenterService)
        {
            _barycenterService = barycenterService;
        }
        [HttpGet("barycenter-locations")]
        public IActionResult GetBarycenterLocations([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int horizonId)
        {
            try
            {
                var locations = _barycenterService.GetBarycenterLocations(startDate, endDate, horizonId);
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
    }

}