using OurSolarSystemAPI.Models;
using SGPdotNET.CoordinateSystem;
using SGPdotNET.Propagation;
using SGPdotNET.TLE;
using SGPdotNET.Util;

namespace OurSolarSystemAPI.Utility
{
    public class CelesTrakScraper
    {
        public EphemerisArtificialSatellite ConvertTleToEphemeris(int noradId, int TleArtificialSatelliteId, string tleFirstLine, string tleSecondLine, DateTime dateTime)
        {
            var tle = new Tle(tleFirstLine, tleSecondLine);
            var sgp4 = new Sgp4(tle);
            EciCoordinate eciCoords = sgp4.FindPosition(dateTime);

            return new EphemerisArtificialSatellite()
            {
                PositionX = eciCoords.Position.X,
                PositionY = eciCoords.Position.Y,
                PositionZ = eciCoords.Position.Z,
                VelocityX = eciCoords.Velocity.X,
                VelocityY = eciCoords.Velocity.Y,
                VelocityZ = eciCoords.Velocity.Z,
                JulianDate = tle.Epoch.ToJulian(),
                DateTime = dateTime
            };


        }


        public TleArtificialSatellite ScrapeNewTleDataForExistingSatellite(int noradId)
        {
            var url = new Uri($"https://celestrak.org/NORAD/elements/gp.php?CATNR={noradId}&FORMAT=tle");
            DateTime scrapedAt = DateTime.Now;
            var provider = new RemoteTleProvider(true, url);
            var tle = provider.GetTle(noradId);

            var sgp4 = new Sgp4(tle);
            EciCoordinate eciCoords = sgp4.FindPosition(DateTime.Now);

            return new TleArtificialSatellite()
            {
                TleFirstLine = tle.Line1,
                TleSecondLine = tle.Line2,
                ScrapedAt = scrapedAt
            };
        }


        public List<ArtificialSatellite> ScrapeSatellitesAndTleData(string urlCelesTrak)
        {
            var satellites = new List<ArtificialSatellite>();
            var url = new Uri(urlCelesTrak);
            DateTime scrapedAt = DateTime.Now;
            var provider = new RemoteTleProvider(true, url);
            var tles = provider.GetTles();

            foreach (KeyValuePair<int, Tle> entry in tles)
            {
                Tle tle = entry.Value;
                var sgp4 = new Sgp4(tle);
                EciCoordinate eciCoords = sgp4.FindPosition(DateTime.Now);

                var tleArtificialSatellite = new TleArtificialSatellite()
                {
                    TleFirstLine = tle.Line1,
                    TleSecondLine = tle.Line2,
                    ScrapedAt = scrapedAt
                };

                satellites.Add(new ArtificialSatellite()
                {
                    Name = tle.Name,
                    NoradId = (int)tle.NoradNumber,
                    BStarDragTerm = tle.BStarDragTerm,
                    Tle = [tleArtificialSatellite],
                    Inclination = tle.Inclination.Degrees,
                    Apogee = tle.ArgumentPerigee.Degrees,
                    NssdcId = tle.IntDesignator,
                    Eccentricity = tle.Eccentricity,
                    MeanAnomaly = tle.MeanAnomaly.Degrees,
                    OrbitNumber = (int)tle.OrbitNumber
                });
            }
            return satellites;
        }



    }

}