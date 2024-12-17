using OurSolarSystemAPI.Models;
using System.Text.RegularExpressions;
namespace OurSolarSystemAPI.Utility
{
    public class HorizonHardcodedData
    {
        public List<Barycenter> BarycenterData() 
        {
            return new List<Barycenter>
            {
                new Barycenter { HorizonId = 0, Name = "Solar System Barycenter" },
                new Barycenter { HorizonId = 1, Name = "Mercury Barycenter" },
                new Barycenter { HorizonId = 2, Name = "Venus Barycenter" },
                new Barycenter { HorizonId = 3, Name = "Earth Barycenter" },
                new Barycenter { HorizonId = 4, Name = "Mars Barycenter" },
                new Barycenter { HorizonId = 5, Name = "Jupiter Barycenter" },
                new Barycenter { HorizonId = 6, Name = "Saturn Barycenter" },
                new Barycenter { HorizonId = 7, Name = "Uranus Barycenter" },
                new Barycenter { HorizonId = 8, Name = "Neptune Barycenter" },
                new Barycenter { HorizonId = 9, Name = "Pluto Barycenter" }
            };
        }

        public Planet MercuryData()
        {
            return new Planet
            {
                HorizonId = 199,
                BarycenterHorizonId = 1,
                Name = "Mercury",
                VolumeMeanRadius = "2439.4±0.1",
                Density = "5.427",
                Mass = "3.302",
                Volume = "6.085",
                EquatorialRadius = "2440.53",
                SiderealRotationPeriod = "58.6463",
                SiderealRotationRate = "0.00000124001",
                MeanSolarDay = "175.9421",
                PolarGravity = null,
                EquatorialGravity = "3.701",
                GeometricAlbedo = "0.106",
                MassRatioToSun = "6023682",
                MeanTemperature = "440",
                AtmosphericPressure = "< 5x10^-15",
                ObliquityToOrbit = "2.11' ± 0.1'",
                MaxAngularDiameter = "11.0",
                MeanSideRealOrbitalPeriod = "87.969257",
                OrbitalSpeed = "47.362",
                HillsSphereRadius = "94.4",
                EscapeSpeed = "4.435",
                GravitationalParameter = "22031.86855",
                Moons = null,
                Ephemeris = null,
                SolarConstantPerihelion = "14462",
                SolarConstantAphelion = "6278",
                SolarConstantMean = "9126",
                MaxPlanetaryIRPerihelion = "12700",
                MaxPlanetaryIRAphelion = "5500",
                MaxPlanetaryIRMean = "8000",
                MinPlanetaryIRPerihelion = "6",
                MinPlanetaryIRAphelion = "6",
                MinPlanetaryIRMean = "6"
            };
        }

        public Planet VenusData()
        {
            return new Planet
            {
                HorizonId = 299,
                BarycenterHorizonId = 2,
                Name = "Venus",
                VolumeMeanRadius = "6051.84 ± 0.01",
                Density = "5.204",
                Mass = "48.685",
                Volume = "92.843",
                EquatorialRadius = "6051.893",
                SiderealRotationPeriod = "243.018484",
                SiderealRotationRate = "-0.00000029924",
                MeanSolarDay = "116.7490",
                PolarGravity = null,
                EquatorialGravity = "8.870",
                GeometricAlbedo = "0.65",
                MassRatioToSun = "408523.72",
                MeanTemperature = "735",
                AtmosphericPressure = "90",
                ObliquityToOrbit = "177.3",
                MaxAngularDiameter = "60.2",
                MeanSideRealOrbitalPeriod = "224.70079922",
                OrbitalSpeed = "35.021",
                HillsSphereRadius = "167.1",
                EscapeSpeed = "10.361",
                GravitationalParameter = "324858.592",
                Moons = null,
                Ephemeris = null,
                SolarConstantPerihelion = "2759",
                SolarConstantAphelion = "2614",
                SolarConstantMean = "2650",
                MaxPlanetaryIRPerihelion = "153",
                MaxPlanetaryIRAphelion = "153",
                MaxPlanetaryIRMean = "153",
                MinPlanetaryIRPerihelion = "153",
                MinPlanetaryIRAphelion = "153",
                MinPlanetaryIRMean = "153"
            };

        }


        public Planet EarthData()
        {
            return new Planet
            {
                HorizonId = 399,
                BarycenterHorizonId = 3,
                Name = "Earth",
                VolumeMeanRadius = "3389.92 ± 0.04",
                Density = "3.933 (5 ± 4)",
                Mass = "6.4171",
                Volume = "16.318",
                EquatorialRadius = "3396.19",
                SiderealRotationPeriod = "24.622962",
                SiderealRotationRate = "0.0000708822",
                MeanSolarDay = "88775.24415",
                PolarGravity = "3.758",
                EquatorialGravity = "3.71",
                GeometricAlbedo = "0.150",
                MassRatioToSun = "3098703.59",
                MeanTemperature = "210",
                AtmosphericPressure = "0.0056",
                ObliquityToOrbit = "25.19",
                MaxAngularDiameter = "17.9",
                MeanSideRealOrbitalPeriod = "686.98",
                OrbitalSpeed = "24.13",
                HillsSphereRadius = "319.8",
                EscapeSpeed = "5.027",
                GravitationalParameter = "42828.375214",
                Moons = null,
                Ephemeris = null,
                SolarConstantPerihelion = "717",
                SolarConstantAphelion = "493",
                SolarConstantMean = "589",
                MaxPlanetaryIRPerihelion = "470",
                MaxPlanetaryIRAphelion = "315",
                MaxPlanetaryIRMean = "390",
                MinPlanetaryIRPerihelion = "30",
                MinPlanetaryIRAphelion = "30",
                MinPlanetaryIRMean = "30"
            };

        }

        public Planet MarsData()
        {
            return new Planet
            {
                HorizonId = 499,
                BarycenterHorizonId = 4,
                Name = "Mars",
                VolumeMeanRadius = "3389.92 ± 0.04",
                Density = "3.933(5 ± 4)",
                Mass = "6.4171",
                Volume = "16.318",
                EquatorialRadius = "3396.19",
                SiderealRotationPeriod = "24.622962",
                SiderealRotationRate = "0.0000708822",
                MeanSolarDay = "88775.24415",
                PolarGravity = "3.758",
                EquatorialGravity = "3.71",
                GeometricAlbedo = "0.150",
                MassRatioToSun = "3098703.59",
                MeanTemperature = "210",
                AtmosphericPressure = "0.0056",
                ObliquityToOrbit = "25.19",
                MaxAngularDiameter = "17.9",
                MeanSideRealOrbitalPeriod = "686.98",
                OrbitalSpeed = "24.13",
                HillsSphereRadius = "319.8",
                EscapeSpeed = "5.027",
                GravitationalParameter = "42828.375214",
                Moons = null,
                Ephemeris = null,
                SolarConstantPerihelion = "717",
                SolarConstantAphelion = "493",
                SolarConstantMean = "589",
                MaxPlanetaryIRPerihelion = "470",
                MaxPlanetaryIRAphelion = "315",
                MaxPlanetaryIRMean = "390",
                MinPlanetaryIRPerihelion = "30",
                MinPlanetaryIRAphelion = "30",
                MinPlanetaryIRMean = "30"
            };
        }

        public Planet JupiterData()
        {
            return new Planet
            {
                HorizonId = 599,
                BarycenterHorizonId = 5,
                Name = "Jupiter",
                VolumeMeanRadius = "69911 ± 6",
                Density = "1.3262 ± 0.0003",
                Mass = "189818722 ± 8817",
                Volume = null,
                EquatorialRadius = "71492 ± 4",
                SiderealRotationPeriod = "~9.9259",
                SiderealRotationRate = "0.00017585",
                MeanSolarDay = "~9.9259",
                PolarGravity = "28.34",
                EquatorialGravity = "24.79",
                GeometricAlbedo = "0.52",
                MassRatioToSun = null,
                MeanTemperature = "165 ± 5",
                AtmosphericPressure = null,
                ObliquityToOrbit = "3.13",
                MaxAngularDiameter = null,
                MeanSideRealOrbitalPeriod = "4332.589",
                OrbitalSpeed = "13.0697",
                HillsSphereRadius = "740",
                EscapeSpeed = "59.5",
                GravitationalParameter = "126686531.900",
                Moons = null,
                Ephemeris = null,
                SolarConstantPerihelion = "56",
                SolarConstantAphelion = "46",
                SolarConstantMean = "51",
                MaxPlanetaryIRPerihelion = "13.7",
                MaxPlanetaryIRAphelion = "13.4",
                MaxPlanetaryIRMean = "13.6",
                MinPlanetaryIRPerihelion = "13.7",
                MinPlanetaryIRAphelion = "13.4",
                MinPlanetaryIRMean = "13.6"
            };
        }


        public Planet SaturnData()
        {
            return new Planet
            {
                HorizonId = 699,
                BarycenterHorizonId = 6,
                Name = "Saturn",
                VolumeMeanRadius = "58232 ± 6",
                Density = "0.687 ± 0.001",
                Mass = "5.6834",
                Volume = null,
                EquatorialRadius = "60268 ± 4",
                SiderealRotationPeriod = "~10.656",
                SiderealRotationRate = "0.000163785",
                MeanSolarDay = "~10.656",
                PolarGravity = "12.14 ± 0.01",
                EquatorialGravity = "10.44",
                GeometricAlbedo = "0.47",
                MassRatioToSun = null,
                MeanTemperature = "134 ± 4",
                AtmosphericPressure = "null",
                ObliquityToOrbit = "26.73",
                MaxAngularDiameter = "null",
                MeanSideRealOrbitalPeriod = "10755.698",
                OrbitalSpeed = "9.68",
                HillsSphereRadius = "1100",
                EscapeSpeed = "35.5",
                GravitationalParameter = "37931206.234",
                Moons = null,
                Ephemeris = null,
                SolarConstantPerihelion = "16.8",
                SolarConstantAphelion = "13.6",
                SolarConstantMean = "15.1",
                MaxPlanetaryIRPerihelion = "4.7",
                MaxPlanetaryIRAphelion = "4.5",
                MaxPlanetaryIRMean = "4.6",
                MinPlanetaryIRPerihelion = "4.7",
                MinPlanetaryIRAphelion = "4.5",
                MinPlanetaryIRMean = "4.6"
            };
        }

        public Planet UranusData()
        {
            return new Planet
            {
                HorizonId = 799,
                BarycenterHorizonId = 7,
                Name = "Uranus",
                VolumeMeanRadius = "25362 ± 12",
                Density = "1.271",
                Mass = "86.813",
                Volume = null,
                EquatorialRadius = "25559 ± 4",
                SiderealRotationPeriod = "~17.24",
                SiderealRotationRate = "-0.000101237",
                MeanSolarDay = "~17.24",
                PolarGravity = "9.19 ± 0.02",
                EquatorialGravity = "8.87",
                GeometricAlbedo = "0.51",
                MassRatioToSun = "null",
                MeanTemperature = "76 ± 2",
                AtmosphericPressure = null,
                ObliquityToOrbit = "97.77",
                MaxAngularDiameter = null,
                MeanSideRealOrbitalPeriod = "30685.4",
                OrbitalSpeed = "6.8",
                HillsSphereRadius = "2700",
                EscapeSpeed = "21.3",
                GravitationalParameter = "5793951.256",
                Moons = null,
                Ephemeris = null,
                SolarConstantPerihelion = "4.09",
                SolarConstantAphelion = "3.39",
                SolarConstantMean = "3.71",
                MaxPlanetaryIRPerihelion = "0.72",
                MaxPlanetaryIRAphelion = "0.55",
                MaxPlanetaryIRMean = "0.63",
                MinPlanetaryIRPerihelion = "0.72",
                MinPlanetaryIRAphelion = "0.55",
                MinPlanetaryIRMean = "0.63"
            };
        }

        public Planet NeptuneData()
        {
            return new Planet
            {
                HorizonId = 899,
                BarycenterHorizonId = 8,
                Name = "Neptune",
                VolumeMeanRadius = "24624 ± 21",
                Density = "1.638",
                Mass = "102.409",
                Volume = "6254",
                EquatorialRadius = "24766 ± 15",
                SiderealRotationPeriod = "~16.11",
                SiderealRotationRate = "0.000108338",
                MeanSolarDay = "~16.11",
                PolarGravity = "11.41 ± 0.03",
                EquatorialGravity = "11.15",
                GeometricAlbedo = "0.41",
                MassRatioToSun = null,
                MeanTemperature = "72 ± 2",
                AtmosphericPressure = null,
                ObliquityToOrbit = "28.32",
                MaxAngularDiameter = null,
                MeanSideRealOrbitalPeriod = "60189",
                OrbitalSpeed = "5.43",
                HillsSphereRadius = "4700",
                EscapeSpeed = "23.5",
                GravitationalParameter = "6835099.97",
                Moons = null,
                Ephemeris = null,
                SolarConstantPerihelion = "1.54",
                SolarConstantAphelion = "1.49",
                SolarConstantMean = "1.51",
                MaxPlanetaryIRPerihelion = "0.52",
                MaxPlanetaryIRAphelion = "0.52",
                MaxPlanetaryIRMean = "0.52",
                MinPlanetaryIRPerihelion = "0.52",
                MinPlanetaryIRAphelion = "0.52",
                MinPlanetaryIRMean = "0.52"
            };
        }

        public Planet PlutoData()
        {
            return new Planet
            {
                HorizonId = 999,
                BarycenterHorizonId = 9,
                Name = "Pluto",
                VolumeMeanRadius = "1188.3 ± 1.6",
                Density = "1.86",
                Mass = "1.307 ± 0.018",
                Volume = "0.697",
                EquatorialRadius = null,
                SiderealRotationPeriod = "153.29335198",
                SiderealRotationRate = "0.0000113856",
                MeanSolarDay = "153.2820",
                PolarGravity = "0.611",
                EquatorialGravity = null,
                GeometricAlbedo = null,
                MassRatioToSun = "0.122",
                MeanTemperature = null,
                AtmosphericPressure = null,
                ObliquityToOrbit = null,
                MaxAngularDiameter = null,
                MeanSideRealOrbitalPeriod = "249.58932",
                OrbitalSpeed = "4.67",
                HillsSphereRadius = null,
                EscapeSpeed = "1.21",
                GravitationalParameter = "869.326",
                Moons = null,
                Ephemeris = null,
                SolarConstantPerihelion = "1.56",
                SolarConstantAphelion = "0.56",
                SolarConstantMean = "0.88",
                MaxPlanetaryIRPerihelion = "0.8",
                MaxPlanetaryIRAphelion = "0.3",
                MaxPlanetaryIRMean = "0.5",
                MinPlanetaryIRPerihelion = "0.8",
                MinPlanetaryIRAphelion = "0.3",
                MinPlanetaryIRMean = "0.5"
            };

        }
    }
}
