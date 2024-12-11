using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using OurSolarSystemAPI.Service;
using System.Data;

namespace OurSolarSystemAPI.Controllers.MySQL 
{
    [ApiController]
    [Route("api/mysql")]
    public class PlanetController : ControllerBase
    {
        private readonly PlanetService _planetService;
        private readonly string _connectionString;

        public PlanetController(PlanetService planetService) 
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

        [HttpGet("locations")]
        public IActionResult GetPlanetLocations([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                // Call the service to get planet locations
                var locations = _planetService.GetPlanetLocations(startDate, endDate);

                // Return the results
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

        [HttpGet("total-planet-mass")]
        public IActionResult GetTotalPlanetMass()
        {
            try
            {
                var totalMass = _planetService.GetTotalPlanetMass();
                return Ok(new { TotalMass = totalMass });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

       

        [HttpGet("overview")]
        public IActionResult GetPlanetOverview()
        {
            try
            {
                var overview = _planetService.GetPlanetOverview();
                return Ok(overview);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }

}