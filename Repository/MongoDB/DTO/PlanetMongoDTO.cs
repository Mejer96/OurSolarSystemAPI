using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OurSolarSystemAPI.Models;

namespace OurSolarSystemAPI.Repository.MongoDB
{
    public class PlanetMongoDTO
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public required int HorizonId { get; set; }
        public List<MoonMongoDTO>? Moons { get; set; }
        public string Name { get; set; }
        public double? VolumeMeanRadius { get; set; }
        public double? Density { get; set; }
        public double? Mass { get; set; }
        public double? Volume { get; set; }
        public double? EquatorialRadius { get; set; }
        public double? SiderealRotationPeriod { get; set; }
        public double? SiderealRotationRate { get; set; }
        public double? MeanSolarDay { get; set; }
        public double? PolarGravity { get; set; }
        public double? EquatorialGravity { get; set; }
        public double? GeometricAlbedo { get; set; }
        public double? MassRatioToSun { get; set; }
        public double? MeanTemperature { get; set; }
        public double? AtmosphericPressure { get; set; }
        public double? ObliquityToOrbit { get; set; }
        public double? MaxAngularDiameter { get; set; }
        public double? MeanSideRealOrbitalPeriod { get; set; }
        public double? OrbitalSpeed { get; set; }
        public double? HillsSphereRadius { get; set; }
        public double? EscapeSpeed { get; set; }
        public double? GravitationalParameter { get; set; }
        public double? SolarConstantPerihelion { get; set; }
        public double? SolarConstantAphelion { get; set; }
        public double? SolarConstantMean { get; set; }
        public double? MaxPlanetaryIRPerihelion { get; set; }
        public double? MaxPlanetaryIRAphelion { get; set; }
        public double? MaxPlanetaryIRMean { get; set; }
        public double? MinPlanetaryIRPerihelion { get; set; }
        public double? MinPlanetaryIRAphelion { get; set; }
        public double? MinPlanetaryIRMean { get; set; }

        public static PlanetMongoDTO ConvertToMongoDTO(Planet planet)
        {
            var moonDTOs = new List<MoonMongoDTO>();

            if (planet.Moons != null)
            {
                foreach (Moon moon in planet.Moons)
                {
                    moonDTOs.Add(MoonMongoDTO.ConvertToMoonMongoDTO(moon));
                }
            }

            return new PlanetMongoDTO
            {
                HorizonId = planet.HorizonId,
                Name = planet.Name,
                Moons = moonDTOs,
                VolumeMeanRadius = planet.VolumeMeanRadius,
                Density = planet.Density,
                Mass = planet.Mass,
                Volume = planet.Volume,
                EquatorialRadius = planet.EquatorialRadius,
                SiderealRotationPeriod = planet.SiderealRotationPeriod,
                SiderealRotationRate = planet.SiderealRotationRate,
                MeanSolarDay = planet.MeanSolarDay,
                PolarGravity = planet.PolarGravity,
                EquatorialGravity = planet.EquatorialGravity,
                GeometricAlbedo = planet.GeometricAlbedo,
                MassRatioToSun = planet.MassRatioToSun,
                MeanTemperature = planet.MeanTemperature,
                AtmosphericPressure = planet.AtmosphericPressure,
                ObliquityToOrbit = planet.ObliquityToOrbit,
                MaxAngularDiameter = planet.MaxAngularDiameter,
                MeanSideRealOrbitalPeriod = planet.MeanSideRealOrbitalPeriod,
                OrbitalSpeed = planet.OrbitalSpeed,
                HillsSphereRadius = planet.HillsSphereRadius,
                EscapeSpeed = planet.EscapeSpeed,
                GravitationalParameter = planet.GravitationalParameter,
                SolarConstantPerihelion = planet.SolarConstantPerihelion,
                SolarConstantAphelion = planet.SolarConstantAphelion,
                SolarConstantMean = planet.SolarConstantMean,
                MaxPlanetaryIRPerihelion = planet.MaxPlanetaryIRPerihelion,
                MaxPlanetaryIRAphelion = planet.MaxPlanetaryIRAphelion,
                MaxPlanetaryIRMean = planet.MaxPlanetaryIRMean,
                MinPlanetaryIRPerihelion = planet.MinPlanetaryIRPerihelion,
                MinPlanetaryIRAphelion = planet.MinPlanetaryIRAphelion,
                MinPlanetaryIRMean = planet.MinPlanetaryIRMean
            };
        }
    }


}