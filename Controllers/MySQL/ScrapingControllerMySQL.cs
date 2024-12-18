using Microsoft.AspNetCore.Mvc;
using OurSolarSystemAPI.Service.MySQL;
using OurSolarSystemAPI.Repository.MySQL;
namespace OurSolarSystemAPI.Controllers;

[ApiController]
[Route("scraper/mysql")]
public class ScrapingControllerMySQL : ControllerBase
{
    private readonly OurSolarSystemContext _context;
    private readonly ScrapingServiceMySQL _scrapingService;
    private readonly HttpClient _httpClient;

    public ScrapingControllerMySQL(ScrapingServiceMySQL scrapingService, OurSolarSystemContext context, HttpClient httpClient) 
    {
        _scrapingService = scrapingService;
        _context = context;
        _httpClient = httpClient;
    }
    [HttpGet("scrape-barycenters-test")]
    public async Task<IActionResult> ScrapeAndAddBarycentersToDBTest()
    {
        var dict = await _scrapingService.ScrapeBarycentersTest(_httpClient);

        return Ok(new
            {
                dict
            });
    }


    [HttpGet("scrape-barycenters")]
    public async Task<IActionResult> ScrapeAndAddBarycentersToDB()
    {
        await _scrapingService.ScrapeBarycenters(_httpClient);

        return Ok(new
            {
                statusCode = 200
            });
    }

    [HttpGet("scrape-sun")]
    public async Task<IActionResult> ScrapeAndAddSunToDB()
    {
        await _scrapingService.ScrapeSun(_httpClient);

        return Ok(new
            {
                statusCode = 200
            });
    }


    [HttpGet("scrape-planets")]
    public async Task<IActionResult> AddPlanetsToDataDB()
    {
        await _scrapingService.ScrapePlanets(_httpClient);

        return Ok(new
            {
                statusCode = 200
            });
    }

    [HttpGet("scrape-moons")]
    public async Task<IActionResult> CreateBarycenterDataDB()
    {
        await _scrapingService.ScrapeMoons(_httpClient);

        return Ok(new
            {
                statusCode = 200
            });
    }

    [HttpGet("scrape-satellites")]
    public async Task<IActionResult> GetHorizonPlanetData()
    {
       await _scrapingService.ScrapeArtificialSatellites(_httpClient);
        
        return Ok(new
            {
                StatusCode = 200
            });
    }
}    
