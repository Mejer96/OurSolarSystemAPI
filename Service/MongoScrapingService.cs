using MongoDB.Bson;
using OurSolarSystemAPI.Models;
using OurSolarSystemAPI.Repository.MongoDB;
using OurSolarSystemAPI.Utility;

namespace OurSolarSystemAPI.Service
{
    public class MongoScrapingService
    {
        private readonly MongoBarycenterRepository _barycenterRepo;
        private readonly MongoPlanetRepository _planetRepo;
        private readonly MongoArtificialSatelliteRepository _satelliteRepo;

        public MongoScrapingService(
            MongoBarycenterRepository barycenterRepo,
            MongoPlanetRepository planetRepo,
            MongoArtificialSatelliteRepository satelliteRepo)
        {
            _barycenterRepo = barycenterRepo;
            _planetRepo = planetRepo;
            _satelliteRepo = satelliteRepo;
        }

        public async Task<bool> ScrapeAndAddBarycentersToMongo(HttpClient httpClient)
        {
            var horizonScraper = new HorizonHardcodedData();
            var horizonParser = new HorizonParser();
            var barycenters = horizonScraper.BarycenterData();

            foreach (var barycenter in barycenters)
            {
                // Generer MongoId for barycenter
                barycenter.MongoId = ObjectId.GenerateNewId().ToString();

                // Gem Barycenter i MongoDB
                await _barycenterRepo.CreateBarycenterAsync(barycenter);

                var ephemeris = new List<EphemerisBarycenter>();
                string url = $"https://ssd.jpl.nasa.gov/api/horizons.api?format=text&COMMAND='{barycenter.HorizonId}'&center='@0'&ephem_type='Vectors'&vec_table=2&step_size=1d&start_time=2024-01-01&stop_time=2024-01-02";
                string apiResponse = await UtilityGetRequest.PerformRequest(url, httpClient);
                List<Dictionary<string, object>> vectors = horizonParser.ExtractEphemeris(apiResponse);

                foreach (var vector in vectors)
                {
                  
                    var ephemerisBarycenter = new EphemerisBarycenter
                    {
                        Barycenter = barycenter, // Embed hele Barycenter-objektet
                        PositionX = Convert.ToDouble(vector["PositionX"]),
                        PositionY = Convert.ToDouble(vector["PositionY"]),
                        PositionZ = Convert.ToDouble(vector["PositionZ"]),
                        VelocityX = Convert.ToDouble(vector["VelocityX"]),
                        VelocityY = Convert.ToDouble(vector["VelocityY"]),
                        VelocityZ = Convert.ToDouble(vector["VelocityZ"]),
                        DateTime = DateTime.Parse(vector["DateTime"].ToString()),
                        JulianDate = Convert.ToDouble(vector["JulianDate"])
                    };

                    
                    await _barycenterRepo.CreateEphemerisBarycenterAsync(ephemerisBarycenter);
                }
            }

            return true;
        }



        public async Task<bool> PopulateEphemerisBarycenters()
        {
            var barycenters = await _barycenterRepo.GetAllBarycentersAsync(); // Fetch all Barycenters from the collection

            foreach (var barycenter in barycenters)
            {
                if (barycenter.Ephemeris == null) continue;

                foreach (var ephemeris in barycenter.Ephemeris)
                {
                    ephemeris.BarycenterId = barycenter.HorizonId; // Assign Barycenter HorizonId
                    await _barycenterRepo.CreateEphemerisBarycenterAsync(ephemeris); // Insert into EphemerisBarycenters
                }
            }
            return true;
        }

        public async Task<bool> PopulateEphemerisPlanets()
        {
            var planets = await _planetRepo.GetAllPlanetsAsync(); // Fetch all Planets from the collection

            foreach (var planet in planets)
            {
                if (planet.Ephemeris == null) continue;

                foreach (var ephemeris in planet.Ephemeris)
                {
                    ephemeris.PlanetId = planet.HorizonId; // Assign Planet HorizonId
                    await _planetRepo.CreateEphemerisPlanetAsync(ephemeris); // Insert into EphemerisPlanets
                }
            }
            return true;
        }




        public async Task<bool> AddHardcodedPlanetsToMongo(HttpClient httpClient)
        {
            var horizonScraper = new HorizonHardcodedData();
            var horizonParser = new HorizonParser();
            var planets = new List<Planet>
    {
        horizonScraper.MercuryData(),
        horizonScraper.VenusData(),
        horizonScraper.EarthData(),
        horizonScraper.MarsData(),
        horizonScraper.JupiterData(),
        horizonScraper.SaturnData(),
        horizonScraper.UranusData(),
        horizonScraper.NeptuneData(),
        horizonScraper.PlutoData()
    };

            foreach (var planet in planets)
            {
                string url = $"https://ssd.jpl.nasa.gov/api/horizons.api?format=text&COMMAND='{planet.HorizonId}'&center='@0'&ephem_type='Vectors'&vec_table=2&step_size=1d&start_time=2024-01-01&stop_time=2024-01-02";
                string apiResponse = await UtilityGetRequest.PerformRequest(url, httpClient);
                List<Dictionary<string, object>> vectorSets = horizonParser.ExtractEphemeris(apiResponse);
                var ephemeris = new List<EphemerisPlanet>();

                foreach (var vectorSet in vectorSets)
                {
                    var ephemerisPlanet = EphemerisPlanet.convertEphemerisDictToObject(vectorSet, planet);

                    // Assign unique Id and link to Planet
                    ephemerisPlanet.MongoId = ObjectId.GenerateNewId().ToString(); // Generate unique Id
                    ephemerisPlanet.PlanetId = planet.HorizonId; // Link to Planet's HorizonId

                    ephemeris.Add(ephemerisPlanet);
                }

                planet.Ephemeris = ephemeris;

                // Insert the planet into MongoDB
                await _planetRepo.CreatePlanetAsync(planet);
            }

            return true;
        }




        public async Task<bool> ScrapeAndAddMoonsToMongo(HttpClient httpClient)
        {
            var horizonParser = new HorizonParser();
            List<MoonContainer> planets = MoonContainer.InstantiatePlanetAndMoonStructs();

            foreach (var planet in planets)
            {
                var moons = new List<Moon>();
                foreach (var moonDesignators in planet.Moons)
                {
                    string url = $"https://ssd.jpl.nasa.gov/api/horizons.api?format=text&COMMAND='{moonDesignators.ID}'&center='@0'&ephem_type='Vectors'&vec_table=2&step_size=1d&start_time=2024-01-01&stop_time=2024-01-02";
                    string apiResponse = await UtilityGetRequest.PerformRequest(url, httpClient);
                    Moon moon = horizonParser.ExtractMoonData(apiResponse);
                    var ephemeris = new List<EphemerisMoon>();
                    List<Dictionary<string, object>> vectorSets = horizonParser.ExtractEphemeris(apiResponse);

                    foreach (var vectorSet in vectorSets)
                    {
                        ephemeris.Add(EphemerisMoon.convertEphemerisDictToObject(vectorSet, moon));
                    }

                    moon.Ephemeris = ephemeris;
                    moons.Add(moon);
                }
                await _planetRepo.AddMoonsToExistingPlanetAsync(moons, planet.HorizonPlanetId);
            }
            return true;
        }

        public async Task<bool> ScrapeAndAddArtificialSatellitesToMongo(HttpClient httpClient)
        {
            var celestrakScraper = new CelesTrakScraper();
            var n2yoScraper = new N2yoScraper();
            string urlStarlinkSatellites = "https://celestrak.org/NORAD/elements/gp.php?INTDES=2019-074&FORMAT=tle";

            List<ArtificialSatellite> satellites = celestrakScraper.ScrapeAndConvertTle(urlStarlinkSatellites);
            foreach (var satellite in satellites)
            {
                string url = $"https://www.n2yo.com/satellite/?s={satellite.NoradId}";
                string htmlContent = await UtilityGetRequest.PerformRequest(url, httpClient);
                Dictionary<string, string> scrapedSatelliteInfoN2yo = n2yoScraper.ExtractSatelliteInfoFromHtml(htmlContent);

                satellite.PlanetId = 3;
                satellite.LaunchDate = scrapedSatelliteInfoN2yo["launchDate"];
                satellite.LaunchSite = scrapedSatelliteInfoN2yo["launchSite"];
                satellite.Period = scrapedSatelliteInfoN2yo["period"];
                satellite.Rcs = scrapedSatelliteInfoN2yo["rcs"];
                satellite.SemiMajorAxis = scrapedSatelliteInfoN2yo["semiMajorAxis"];
                satellite.Perigee = scrapedSatelliteInfoN2yo["perigee"];
                await _satelliteRepo.CreateSatelliteAsync(satellite);
            }
            return true;
        }
    }
}
