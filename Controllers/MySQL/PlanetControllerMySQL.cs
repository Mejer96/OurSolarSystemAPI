using Microsoft.AspNetCore.Mvc;
using OurSolarSystemAPI.Service.MySQL;
using OurSolarSystemAPI.Models;
using OurSolarSystemAPI.Repository.MySQL;
using OurSolarSystemAPI.Controllers.ExceptionHandler;

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
            try 
            {
                Planet planet = await _planetService.GetByHorizonId(horizonId);
                return Ok(planet);
            }
            catch (Exception exception)
            {
                return ControllerExceptionHandler.HandleException(exception, this);
            }
            
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> RequestAll() 
        {
            try
            {
                List<Planet> planets = await _planetService.GetAll();
                return Ok(planets);
            }
            catch (Exception exception)
            {
                return ControllerExceptionHandler.HandleException(exception, this);
            }
        }

        [HttpGet("get-location-by-horizon-id-and-date")]
        public async Task<IActionResult> RequestLocationByHorizonIdAndDate(int horizonId, int day, int month, int year) 
        {
            try 
            {
                Planet planet = await _planetService.GetLocationByHorizonIdAndDate(horizonId, new DateTime(year, month, day));
                return Ok(planet);
            }
            catch (Exception exception)
            {
                return ControllerExceptionHandler.HandleException(exception, this);
            }
        }

        [HttpGet("get-current-location-by-horizon-id")]
        public async Task<IActionResult> RequestCurrentLocation(int horizonId) 
        {
            try 
            {
                Planet planet = await _planetService.GetLocationByHorizonIdAndDate(horizonId, DateTime.Now.Date);
                return Ok(planet);
            }
            catch (Exception exception)
            {
                return ControllerExceptionHandler.HandleException(exception, this);
            }
        }

        [HttpGet("get-distance-between")]
        public async Task<IActionResult> RequestDistanceBetween(int firstHorizonId, int secondHorizonId, int day, int month, int year) 
        {
            try 
            {
                DistanceResult result = await _planetService.GetDistance(firstHorizonId, secondHorizonId, new DateTime(year, month, day));
                return Ok(result);
            }
            catch (Exception exception)
            {
                return ControllerExceptionHandler.HandleException(exception, this);
            }
        }


        [HttpGet("get-locations-by-horizon-id")]
        public async Task<IActionResult> RequestLocationsByHorizonId(int horizonId) 
        {   
            try 
            {
                Planet planet = await _planetService.GetLocationsByHorizonId(horizonId);
                return Ok(planet);
            }
            catch (Exception exception)
            {
                return ControllerExceptionHandler.HandleException(exception, this);
            }
        }

        [HttpGet("get-by-name")]
        public async Task<IActionResult> RequestByName(string name) 
        {
            try 
            {
                Planet planet = await _planetService.GetByName(name);
                return Ok(planet);
            }
            catch (Exception exception)
            {
                return ControllerExceptionHandler.HandleException(exception, this);
            }
        }


        [HttpGet("get-locations-with-pagination-by-horizon-id")]
        public async Task<IActionResult> RequestPlanetEphemerisWithPagination(int horizonId, int pageNumber, int pageSize) 
        {
            try 
            {
                List<EphemerisPlanet> data = await _planetService.GetEphemerisWithPagination(horizonId, pageNumber, pageSize);
                return Ok(data);
            }
            catch (Exception exception)
            {
                return ControllerExceptionHandler.HandleException(exception, this);
            }
        }
    }

}