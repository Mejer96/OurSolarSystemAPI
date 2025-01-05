using Microsoft.AspNetCore.Mvc;
using OurSolarSystemAPI.Models;
using OurSolarSystemAPI.Service;

namespace OurSolarSystemAPI.Controllers.MySQL 
{
    [ApiController]
    [Route("api/mysql")]
    public class ArtificialSatelliteControllerMySQL : ControllerBase
    {
        private readonly ArtificialSatelliteService _satelliteService;

        public ArtificialSatelliteControllerMySQL(ArtificialSatelliteService satelliteService) 
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
        
    }

}