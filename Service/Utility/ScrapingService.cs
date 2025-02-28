using OurSolarSystemAPI.Models;
using OurSolarSystemAPI.Utility;
namespace OurSolarSystemAPI.Service
{
    public class ScrapingService
    {
        public async Task<Star> ScrapeSun(HttpClient httpClient)
        {
            var horizonScraper = new HorizonHardcodedData();
            var horizonParser = new HorizonParser();
            Star sun = horizonScraper.SunData();
            var ephemeris = new List<EphemerisSun>();
            string url = $"https://ssd.jpl.nasa.gov/api/horizons.api?format=text&COMMAND='{sun.HorizonId}'&center='@0'&ephem_type='Vectors'&vec_table=2&step_size=1d&start_time=2025-01-01&stop_time=2026-01-01";
            string apiResponse = await UtilityGetRequest.PerformRequest(url, httpClient);
            List<Dictionary<string, object>> vectors = horizonParser.ExtractEphemeris(apiResponse);

            foreach (var vector in vectors)
            {
                ephemeris.Add(EphemerisSun.ConvertEphemerisDictToObject(vector, sun));
            }

            sun.Ephemeris = ephemeris;

            return sun;
        }

        public async Task<List<Barycenter>> ScrapeBarycenters(HttpClient httpClient)
        {
            var horizonScraper = new HorizonHardcodedData();
            var horizonParser = new HorizonParser();
            List<Barycenter> barycenters = horizonScraper.BarycenterData();


            foreach (var barycenter in barycenters)
            {
                var ephemeris = new List<EphemerisBarycenter>();
                string url = $"https://ssd.jpl.nasa.gov/api/horizons.api?format=text&COMMAND='{barycenter.HorizonId}'&center='@0'&ephem_type='Vectors'&vec_table=2&step_size=1d&start_time=2025-01-01&stop_time=2026-01-01";
                string apiResponse = await UtilityGetRequest.PerformRequest(url, httpClient);
                List<Dictionary<string, object>> vectors = horizonParser.ExtractEphemeris(apiResponse);

                foreach (var vector in vectors)
                {
                    ephemeris.Add(EphemerisBarycenter.ConvertEphemerisDictToObject(vector, barycenter));
                }
                barycenter.Ephemeris = ephemeris;
            }
            return barycenters;
        }

        public async Task<Dictionary<string, object>> ScrapeBarycentersTest(HttpClient httpClient)
        {
            var horizonScraper = new HorizonHardcodedData();
            var horizonParser = new HorizonParser();
            List<Barycenter> barycenters = horizonScraper.BarycenterData();
            var dict = new Dictionary<string, object>();

            foreach (var barycenter in barycenters)
            {
                var ephemeris = new List<EphemerisBarycenter>();
                string url = $"https://ssd.jpl.nasa.gov/api/horizons.api?format=text&COMMAND='{barycenter.HorizonId}'&center='@0'&ephem_type='Vectors'&vec_table=2&step_size=1d&start_time=2025-01-01&stop_time=2026-01-01";
                string apiResponse = await UtilityGetRequest.PerformRequest(url, httpClient);
                List<Dictionary<string, object>> vectors = horizonParser.ExtractEphemerisTest2(apiResponse);
                dict.Add(barycenter.Name, vectors);

            }
            return dict;
        }

        public async Task<List<Planet>> ScrapePlanets(HttpClient httpClient)
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
                string url = $"https://ssd.jpl.nasa.gov/api/horizons.api?format=text&COMMAND='{planet.HorizonId}'&center='@0'&ephem_type='Vectors'&vec_table=2&step_size=1d&start_time=2025-01-01&stop_time=2026-01-01";
                string apiResponse = await UtilityGetRequest.PerformRequest(url, httpClient);
                List<Dictionary<string, object>> vectorSets = horizonParser.ExtractEphemeris(apiResponse);
                var ephemeris = new List<EphemerisPlanet>();

                foreach (var vectorSet in vectorSets)
                {
                    ephemeris.Add(EphemerisPlanet.ConvertEphemerisDictToObject(vectorSet, planet));
                }
                planet.Ephemeris = ephemeris;
            }
            return planets;
        }

        public async Task<List<List<Moon>>> ScrapeMoons(HttpClient httpClient)
        {
            var horizonParser = new HorizonParser();
            List<MoonContainer> moonContainers = MoonContainer.InstantiatePlanetAndMoonStructs();
            var moonLists = new List<List<Moon>>();

            foreach (var mooncontainer in moonContainers)
            {
                var moons = new List<Moon>();
                foreach (var moonDesignators in mooncontainer.Moons)
                {
                    string url = $"https://ssd.jpl.nasa.gov/api/horizons.api?format=text&COMMAND='{moonDesignators.ID}'&center='@0'&ephem_type='Vectors'&vec_table=2&step_size=1d&start_time=2025-01-01&stop_time=2026-01-01";
                    string apiResponse = await UtilityGetRequest.PerformRequest(url, httpClient);
                    Moon moon = horizonParser.ExtractMoonData(apiResponse);
                    var ephemeris = new List<EphemerisMoon>();
                    List<Dictionary<string, object>> vectorSets = horizonParser.ExtractEphemeris(apiResponse);
                    moon.PlanetHorizonId = mooncontainer.PlanetHorizonId;
                    moon.BarycenterHorizonId = mooncontainer.BarycenterHorizonId;
                    moon.HorizonId = moonDesignators.ID;
                    moon.PlanetName = mooncontainer.Name;

                    foreach (var vectorSet in vectorSets)
                    {
                        ephemeris.Add(EphemerisMoon.ConvertEphemerisDictToObject(vectorSet, moon));
                    }
                    moon.Ephemeris = ephemeris;
                    moons.Add(moon);
                }
                moonLists.Add(moons);
            }
            return moonLists;
        }

        public async Task<List<ArtificialSatellite>> ScrapeArtificialSatellites(HttpClient httpClient)
        {
            var celestrakScraper = new CelesTrakScraper();
            var n2yoScraper = new N2yoScraper();
            string urlStarlinkSatelittes = "https://celestrak.org/NORAD/elements/gp.php?INTDES=2019-074&FORMAT=tle";

            List<ArtificialSatellite> satellites = celestrakScraper.ScrapeSatellitesAndTleData(urlStarlinkSatelittes);
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
            }
            return satellites;
        }
    }
}