using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OurSolarSystemAPI.Repository.MongoDB;
using OurSolarSystemAPI.Service;

namespace OurSolarSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MongoScrapingController : ControllerBase
    {
        private readonly MongoScrapingService _mongoScrapingService;
        private readonly HttpClient _httpClient;
        private readonly MongoBarycenterRepository _barycenterRepo; // Add this field


        public MongoScrapingController(MongoScrapingService scrapingService, MongoBarycenterRepository barycenterRepo, HttpClient httpClient)
        {
            _mongoScrapingService = scrapingService;
            _barycenterRepo = barycenterRepo; // Inject repository
            _httpClient = httpClient;
        }

        // Scrape Barycenters for MongoDB
        [HttpGet("scrape-barycenters")]
        public async Task<IActionResult> ScrapeAndAddBarycentersToMongo()
        {
            bool result = await _mongoScrapingService.ScrapeAndAddBarycentersToMongo(_httpClient);
            return result
                ? Ok(new { statusCode = 200 })
                : StatusCode(500, new { statusCode = 500, message = "Failed to scrape barycenters for MongoDB" });
        }

        // Add Hardcoded Planets to MongoDB
        [HttpGet("scrape-planets")]
        public async Task<IActionResult> AddPlanetsToMongoDB()
        {
            bool result = await _mongoScrapingService.AddHardcodedPlanetsToMongo(_httpClient);
            return result
                ? Ok(new { statusCode = 200 })
                : StatusCode(500, new { statusCode = 500, message = "Failed to scrape planets for MongoDB" });
        }

        [HttpPost("populate-ephemeris-barycenters")]
        public async Task<IActionResult> PopulateEphemerisBarycenters()
        {
            var success = await _mongoScrapingService.PopulateEphemerisBarycenters();
            if (success)
            {
                return Ok(new { message = "Ephemeris Barycenters populated successfully" });
            }
            return StatusCode(500, new { message = "Failed to populate Ephemeris Barycenters" });
        }

        [HttpPost("populate-ephemeris-planets")]
        public async Task<IActionResult> PopulateEphemerisPlanets()
        {
            var success = await _mongoScrapingService.PopulateEphemerisPlanets();
            if (success)
            {
                return Ok(new { message = "Ephemeris Planets populated successfully" });
            }
            return StatusCode(500, new { message = "Failed to populate Ephemeris Planets" });
        }



        // Scrape Moons for MongoDB
        [HttpGet("scrape-moons")]
        public async Task<IActionResult> ScrapeAndAddMoonsToMongo()
        {
            bool result = await _mongoScrapingService.ScrapeAndAddMoonsToMongo(_httpClient);
            return result
                ? Ok(new { statusCode = 200 })
                : StatusCode(500, new { statusCode = 500, message = "Failed to scrape moons for MongoDB" });
        }

        // Scrape Artificial Satellites for MongoDB
        [HttpGet("scrape-satellites")]
        public async Task<IActionResult> ScrapeAndAddArtificialSatellitesToMongo()
        {
            bool result = await _mongoScrapingService.ScrapeAndAddArtificialSatellitesToMongo(_httpClient);
            return result
                ? Ok(new { statusCode = 200 })
                : StatusCode(500, new { statusCode = 500, message = "Failed to scrape artificial satellites for MongoDB" });
        }

        [HttpGet("get-ephemeris-barycenters")]
        public async Task<IActionResult> GetEphemerisBarycenters()
        {
            var ephemerisBarycenters = await _barycenterRepo.GetAllEphemerisBarycentersAsync();
            return Ok(ephemerisBarycenters);
        }

    }
}
