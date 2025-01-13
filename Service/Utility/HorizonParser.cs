using OurSolarSystemAPI.Models;
using System.Globalization;
using System.Text.RegularExpressions;

namespace OurSolarSystemAPI.Utility
{
    public class HorizonParser
    {
        public List<Dictionary<string, object>> ExtractEphemeris(string apiResponse)
        {
            List<Dictionary<string, object>> ephemerisData = new();

            Regex regexEphemerisSection = new Regex(@"\$\$SOE(.*?)\$\$EOE", RegexOptions.Singleline);
            Regex regexjulianDate = new Regex(@"(.*?)= A\.D\.");
            Regex regexdate = new Regex(@"= A\.D\. (.*?) 00:00");

            Regex regexPostionX = new Regex(@"X =(.*?)Y");
            Regex regexPostionY = new Regex(@"Y =(.*?)Z");
            Regex regexPostionZ = new Regex(@"Z =\s*(.+)");

            Regex regexVelocityX = new Regex(@"VX=(.*?)VY");
            Regex regexVelocityY = new Regex(@"VY=(.*?)VZ");
            Regex regexVelocityZ = new Regex(@"VZ=\s*(.+)");


            string ephemerisSection = regexEphemerisSection.Match(apiResponse).Groups[0].Value;
            MatchCollection julianDates = regexjulianDate.Matches(ephemerisSection);
            MatchCollection dates = regexdate.Matches(ephemerisSection);

            MatchCollection positionsX = regexPostionX.Matches(ephemerisSection);
            MatchCollection positionsY = regexPostionY.Matches(ephemerisSection);
            MatchCollection positionsZ = regexPostionZ.Matches(ephemerisSection);

            MatchCollection velocitiesX = regexVelocityX.Matches(ephemerisSection);
            MatchCollection velocitiesY = regexVelocityY.Matches(ephemerisSection);
            MatchCollection velocitiesZ = regexVelocityZ.Matches(ephemerisSection);

            for (int i = 0; i < julianDates.Count; i++)
            {
                double positionX = Convert.ToDouble(positionsX[i].Groups[1].Value.Trim());
                double positionY = Convert.ToDouble(positionsY[i].Groups[1].Value.Trim());
                double positionZ = Convert.ToDouble(positionsZ[i].Groups[1].Value.Trim());

                double velocityX = Convert.ToDouble(velocitiesX[i].Groups[1].Value.Trim());
                double velocityY = Convert.ToDouble(velocitiesX[i].Groups[1].Value.Trim());
                double velocityZ = Convert.ToDouble(velocitiesX[i].Groups[1].Value.Trim());

                string fullDate = dates[i].Groups[1].Value.Trim();
                DateTime datetime = ParseEphimerisDateToDateTime(fullDate);

                var ephemerisDict = new Dictionary<string, object>
                {
                    { "julianDate", julianDates[i].Groups[1].Value.Trim() },
                    { "dateTime", datetime },
                    { "positionX", positionX },
                    { "positionY", positionY },
                    { "positionZ", positionZ },
                    { "velocityX", velocityX },
                    { "velocityY", velocityY },
                    { "velocityZ", velocityZ }
                };
                ephemerisData.Add(ephemerisDict);

            }
            return ephemerisData;
        }

        public List<Dictionary<string, object>> ExtractEphemerisTest(string apiResponse)
        {
            List<Dictionary<string, object>> ephemerisData = new();

            Regex regexEphemerisSection = new Regex(@"\$\$SOE(.*?)\$\$EOE", RegexOptions.Singleline);
            Regex regexjulianDate = new Regex(@"(.*?)= A\.D\.");
            Regex regexdate = new Regex(@"= A\.D\. (.*?) 00:00");

            Regex regexPostionX = new Regex(@"X =(.*?)Y");
            Regex regexPostionY = new Regex(@"Y =(.*?)Z");
            Regex regexPostionZ = new Regex(@"Z =\s*(.+)");

            Regex regexVelocityX = new Regex(@"VX=(.*?)VY");
            Regex regexVelocityY = new Regex(@"VY=(.*?)VZ");
            Regex regexVelocityZ = new Regex(@"VZ=\s*(.+)");


            string ephemerisSection = regexEphemerisSection.Match(apiResponse).Groups[0].Value;
            MatchCollection julianDates = regexjulianDate.Matches(ephemerisSection);
            MatchCollection dates = regexdate.Matches(ephemerisSection);

            MatchCollection positionsX = regexPostionX.Matches(ephemerisSection);
            MatchCollection positionsY = regexPostionY.Matches(ephemerisSection);
            MatchCollection positionsZ = regexPostionZ.Matches(ephemerisSection);

            MatchCollection velocitiesX = regexVelocityX.Matches(ephemerisSection);
            MatchCollection velocitiesY = regexVelocityY.Matches(ephemerisSection);
            MatchCollection velocitiesZ = regexVelocityZ.Matches(ephemerisSection);

            for (int i = 0; i < julianDates.Count; i++)
            {
                var positionX = positionsX[i].Groups[1].Value.Trim();
                var positionY = positionsY[i].Groups[1].Value.Trim();
                var positionZ = positionsZ[i].Groups[1].Value.Trim();

                var velocityX = velocitiesX[i].Groups[1].Value.Trim();
                var velocityY = velocitiesX[i].Groups[1].Value.Trim();
                var velocityZ = velocitiesX[i].Groups[1].Value.Trim();

                string fullDate = dates[i].Groups[1].Value.Trim();
                DateTime datetime = ParseEphimerisDateToDateTime(fullDate);

                var ephemerisDict = new Dictionary<string, object>
                {
                    { "julianDate", julianDates[i].Groups[1].Value.Trim() },
                    { "dateTime", datetime },
                    { "positionX", positionX },
                    { "positionY", positionY },
                    { "positionZ", positionZ },
                    { "velocityX", velocityX },
                    { "velocityY", velocityY },
                    { "velocityZ", velocityZ }
                };
                ephemerisData.Add(ephemerisDict);

            }
            return ephemerisData;
        }

        public List<Dictionary<string, object>> ExtractEphemerisTest2(string apiResponse)
        {
            List<Dictionary<string, object>> ephemerisData = new();

            Regex regexEphemerisSection = new Regex(@"\$\$SOE(.*?)\$\$EOE", RegexOptions.Singleline);
            Regex regexjulianDate = new Regex(@"(.*?)= A\.D\.");
            Regex regexdate = new Regex(@"= A\.D\. (.*?) 00:00");

            Regex regexPostionX = new Regex(@"X =(.*?)Y");
            Regex regexPostionY = new Regex(@"Y =(.*?)Z");
            Regex regexPostionZ = new Regex(@"Z =\s*(.+)");

            Regex regexVelocityX = new Regex(@"VX=(.*?)VY");
            Regex regexVelocityY = new Regex(@"VY=(.*?)VZ");
            Regex regexVelocityZ = new Regex(@"VZ=\s*(.+)");


            string ephemerisSection = regexEphemerisSection.Match(apiResponse).Groups[0].Value;
            MatchCollection julianDates = regexjulianDate.Matches(ephemerisSection);
            MatchCollection dates = regexdate.Matches(ephemerisSection);

            MatchCollection positionsX = regexPostionX.Matches(ephemerisSection);
            MatchCollection positionsY = regexPostionY.Matches(ephemerisSection);
            MatchCollection positionsZ = regexPostionZ.Matches(ephemerisSection);

            MatchCollection velocitiesX = regexVelocityX.Matches(ephemerisSection);
            MatchCollection velocitiesY = regexVelocityY.Matches(ephemerisSection);
            MatchCollection velocitiesZ = regexVelocityZ.Matches(ephemerisSection);

            for (int i = 0; i < julianDates.Count; i++)
            {
                Double.Parse("1.234567E-06", NumberStyles.Float, CultureInfo.InvariantCulture);
                // Parsing positions
                // Parsing positions
                double positionX = Double.Parse(positionsX[i].Groups[1].Value.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture);
                double positionY = Double.Parse(positionsY[i].Groups[1].Value.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture);
                double positionZ = Double.Parse(positionsZ[i].Groups[1].Value.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture);

                // Parsing velocities
                double velocityX = Double.Parse(velocitiesX[i].Groups[1].Value.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture);
                double velocityY = Double.Parse(velocitiesY[i].Groups[1].Value.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture);
                double velocityZ = Double.Parse(velocitiesZ[i].Groups[1].Value.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture);

                string fullDate = dates[i].Groups[1].Value.Trim();
                DateTime datetime = ParseEphimerisDateToDateTime(fullDate);

                var ephemerisDict = new Dictionary<string, object>
                {
                    { "julianDate", julianDates[i].Groups[1].Value.Trim() },
                    { "dateTime", datetime },
                    { "positionX", positionX },
                    { "positionY", positionY },
                    { "positionZ", positionZ },
                    { "velocityX", velocityX },
                    { "velocityY", velocityY },
                    { "velocityZ", velocityZ }
                };
                ephemerisData.Add(ephemerisDict);

            }
            return ephemerisData;
        }

        public DateTime ParseEphimerisDateToDateTime(string ephemerisDate)
        {
            var regexEphmerisData = new Regex(@"^(\d{4})-([a-zA-Z]{3})-(\d{2})$");
            var monthsToNumbers = new Dictionary<string, int>
                {
                    { "Jan", 1 },
                    { "Feb", 2 },
                    { "Mar", 3 },
                    { "Apr", 4 },
                    { "May", 5 },
                    { "Jun", 6 },
                    { "Jul", 7 },
                    { "Aug", 8 },
                    { "Sep", 9 },
                    { "Oct", 10 },
                    { "Nov", 11 },
                    { "Dec", 12 }
                };

            Match match = regexEphmerisData.Match(ephemerisDate);
            int year = int.Parse(match.Groups[1].Value.Trim());
            int month = monthsToNumbers[match.Groups[2].Value.Trim()];
            int day = int.Parse(match.Groups[3].Value.Trim());

            return new DateTime(year, month, day);
        }

        public Moon ExtractMoonData(string apiResponse)
        {
            Regex regexMoonName = new Regex(@"Target body name:(.*?){source:");
            Regex regexAllMoonAttributes = new Regex(@"\*{5,}([\s\S]*?)\*{5,}");
            Regex regexMeanRadius = new Regex(@"Mean radius.*?=(.*?)Density");
            Regex regexDensity = new Regex(@"Density.*?=\s*(.+)");
            Regex regexGM = new Regex(@"GM.*?=(.*?)Geometric Albedo");
            Regex regexGeometricAlbedo = new Regex(@"Geometric Albedo.*?=\s*(.+)");
            Regex regexSemiMajorAxis = new Regex(@"Semi-major axis.*?~(.*?)Orbital period");
            Regex regexOrbitalPeriod = new Regex(@"Orbital period.*?~\s*(.+)");
            Regex regexEccentricity = new Regex(@"Eccentricity.*?~(.*?)Rotational period");
            Regex regexRotationalPeriod = new Regex(@"Rotational period.*?=\s*(.+)");
            Regex regexInclination = new Regex(@"Inclination.*?~\s*(.+)");

            string moonName = regexMoonName.Match(apiResponse).Groups[1].Value.Trim();
            moonName = Regex.Replace(moonName, @"\s\(\d{3}\)", "").ToLower();
            string moonAttributes = regexAllMoonAttributes.Match(apiResponse).Groups[0].Value.Trim();
            string meanRadius = regexMeanRadius.Match(moonAttributes).Groups[1].Value.Trim();
            string density = regexDensity.Match(moonAttributes).Groups[1].Value.Trim();
            string gm = regexGM.Match(moonAttributes).Groups[1].Value.Trim();
            string geometricAlbedo = regexGeometricAlbedo.Match(moonAttributes).Groups[1].Value.Trim();
            string semiMajorAxis = regexSemiMajorAxis.Match(moonAttributes).Groups[1].Value.Trim();
            string orbitalPeriod = regexOrbitalPeriod.Match(moonAttributes).Groups[1].Value.Trim();
            string eccentricity = regexEccentricity.Match(moonAttributes).Groups[1].Value.Trim();
            string rotationalPeriod = regexRotationalPeriod.Match(moonAttributes).Groups[1].Value.Trim();
            string inclination = regexInclination.Match(moonAttributes).Groups[1].Value.Trim();

            return new Moon
            {
                Name = moonName,
            };
        }



    }
}