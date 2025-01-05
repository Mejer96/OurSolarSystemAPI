using Microsoft.AspNetCore.Mvc;
using OurSolarSystemAPI.Service.MongoDB;
using OurSolarSystemAPI.Repository.MongoDB;

namespace OurSolarSystemAPI.Controllers.MongoDB 
{
    [ApiController]
    [Route("mongodb/api/planet")]
    public class PlanetControllerMongoDB : ControllerBase
    {
        private readonly PlanetServiceMongoDB _planetService;

        public PlanetControllerMongoDB(PlanetServiceMongoDB planetService) 
        {
            _planetService = planetService;
        }


        [HttpGet("get-by-horizon-id")]
        public async Task<IActionResult> RequestByHorizonId(int horizonId) 
        {
            PlanetMongoDTO planet = await _planetService.GetByHorizonId(horizonId);

            return Ok(planet);
        }

        [HttpGet("get-location-by-horizon-id-and-date")]
        public async Task<IActionResult> RequestLocationByHorizonIdAndDate(int horizonId, int day, int month, int year) 
        {
            EphemerisMongoDTO ephemeris = await _planetService.GetLocationByHorizonIdAndDate(horizonId, new DateTime(year, month, day));

            return Ok(ephemeris);
        }

        [HttpGet("get-current-location-by-horizon-id")]
        public async Task<IActionResult> RequestCurrentLocation(int horizonId) 
        {
            EphemerisMongoDTO ephemeris = await _planetService.GetLocationByHorizonIdAndDate(horizonId, DateTime.Now.Date);

            return Ok(ephemeris);
        }

        [HttpGet("get-distance-between")]
        public async Task<IActionResult> RequestDistanceBetween(int firstHorizonId, int secondHorizonId, int day, int month, int year) 
        {
            (DateTime fetchedDate, double distance) = await _planetService.GetDistanceBetween(firstHorizonId, secondHorizonId, new DateTime(year, month, day));

            return Ok(new Dictionary<string, object>
                {
                    { "date", fetchedDate },
                    { "distance", distance }
                });
        }


        [HttpGet("get-locations-by-horizon-id")]
        public async Task<IActionResult> RequestLocationsByHorizonId(int horizonId) 
        {
            List<EphemerisMongoDTO> ephemeris = await _planetService.GetLocationsByHorizonId(horizonId);

            return Ok(ephemeris);
        }

        [HttpGet("get-by-name")]
        public async Task<IActionResult> RequestByName(string name) 
        {
            PlanetMongoDTO planet = await _planetService.GetByName(name);

            return Ok(planet);
        }

    }

}