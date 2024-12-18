using OurSolarSystemAPI.Models;
using OurSolarSystemAPI.Repository.MySQL;

namespace OurSolarSystemAPI.Service.MySQL 
{
    public class ScrapingServiceMySQL 
    {
        private readonly ArtificialSatelliteRepositoryMySQL _artificialSatelliteRepo;
        private readonly BarycenterRepositoryMySQL _barycenterRepo;
        private readonly PlanetRepositoryMySQL _planetRepo;
        private readonly MoonRepositoryMySQL _moonRepo;
        private readonly ScrapingService _scrapingService;

        public ScrapingServiceMySQL(
            ArtificialSatelliteRepositoryMySQL artificialSatelliteRepo,
            BarycenterRepositoryMySQL barycenterRepo,
            PlanetRepositoryMySQL planetRepo,
            MoonRepositoryMySQL moonRepo,
            ScrapingService scrapingService) 
        {
             _artificialSatelliteRepo = artificialSatelliteRepo;
             _barycenterRepo = barycenterRepo;
             _planetRepo = planetRepo;
             _moonRepo = moonRepo;
             _scrapingService = scrapingService;
        }

        public async Task ScrapeBarycenters(HttpClient httpClient) 
        {
            var solarSystemBaryCenter = new SolarSystemBarycenter();
            solarSystemBaryCenter.HorizonId = 0;
            solarSystemBaryCenter.Name = "solar system barycenter";
            List<Barycenter> barycenters = await _scrapingService.ScrapeBarycenters(httpClient);
            await _barycenterRepo.CreateSolarSystemBarycenter(solarSystemBaryCenter);

            foreach (Barycenter barycenter in barycenters) 
            {
                await _barycenterRepo.AddBarycenterToExistingSolarSystemBarycenter(barycenter);
            }
        }

        public async Task<Dictionary<string, object>> ScrapeBarycentersTest(HttpClient httpClient) 
        {
            return await _scrapingService.ScrapeBarycentersTest(httpClient);
        }

        public async Task ScrapeSun(HttpClient httpClient) 
        {
            Star sun = await _scrapingService.ScrapeSun(httpClient);
            List<Planet> planets = await _scrapingService.ScrapePlanets(httpClient);
            await _barycenterRepo.AddSunToExistingSolarSystemBarycenter(sun);
        }


        public async Task ScrapePlanets(HttpClient httpClient) 
        {
            List<Planet> planets = await _scrapingService.ScrapePlanets(httpClient);


            foreach (Planet planet in planets) 
            {
                await _barycenterRepo.AddPlanetToExistingBarycenter(planet, planet.BarycenterHorizonId);
            }
        }

        public async Task ScrapeMoons(HttpClient httpClient) 
        {
            List<List<Moon>> moonlists = await _scrapingService.ScrapeMoons(httpClient);

            foreach (List<Moon> moonList in moonlists) 
            {
                await _moonRepo.addMoonsToExistingPlanetAndBarycenter(moonList, moonList[0].PlanetHorizonId, moonList[0].BarycenterHorizonId);
            }
        }

        public async Task ScrapeArtificialSatellites(HttpClient httpClient) 
        {
            List<ArtificialSatellite> satellites = await _scrapingService.ScrapeArtificialSatellites(httpClient);

            foreach (ArtificialSatellite satellite in satellites) 
            {
                _artificialSatelliteRepo.AddSatellite(satellite);
            }
        }

    
    }

}