using Microsoft.AspNetCore.Mvc;
using OurSolarSystemAPI.Models;
using OurSolarSystemAPI.Service;

namespace OurSolarSystemAPI.Controllers.MySQL 
{
    [ApiController]
    [Route("api/mysql")]
    public class ArtificialSatelliteController : ControllerBase
    {
        private readonly ArtificialSatelliteService _satelliteService;

        public ArtificialSatelliteController(ArtificialSatelliteService satelliteService) 
        {
            _satelliteService = satelliteService;
        }

        [HttpGet("get-satellite-by-norad-id")]
        public IActionResult RequestSatelliteByNoradId(int noradId) 
        {
            return Ok(_satelliteService.RequestSatelliteByNoradId(noradId));
        }

        [HttpGet("get-satellite-location-by-norad-id-and-date")]
        public IActionResult RequestSatelliteByNoradIdAndDatetime(int noradId, DateTime dateTime) 
        {
            return Ok(_satelliteService.RequestSatelliteByNoradIdAndDateTime(noradId, dateTime));
        }

        [HttpPost("log-search")]
        public IActionResult LogSatelliteSearch([FromQuery] int noradId)
        {
            try
            {
                _satelliteService.LogSatelliteSearch(noradId);
                return Ok("Search logged successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("sum-satellite-orbits")]
        public IActionResult GetSumOfSatelliteOrbits()
        {
            try
            {
                var totalOrbits = _satelliteService.GetSumOfSatelliteOrbits();
                return Ok(new { TotalOrbits = totalOrbits });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }

}