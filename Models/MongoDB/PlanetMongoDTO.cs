using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OurSolarSystemAPI.Models.MongoDB
{
    public class PlanetMongoDTO
    {
        [BsonId]
        public ObjectId Id {get; set; }
        public required int HorizonId { get; set; }
        public List<MoonMongoDTO>? Moons { get; set; }
        public string Name { get; set; }
        public string? VolumeMeanRadius { get; set; }
        public string? Density { get; set; }
        public string? Mass { get; set; }
        public string? Volume { get; set; }
        public string? EquatorialRadius { get; set; }
        public string? SiderealRotationPeriod { get; set; }
        public string? SiderealRotationRate { get; set; }
        public string? MeanSolarDay { get; set; }
        public string? PolarGravity { get; set; }
        public string? EquatorialGravity { get; set; }
        public string? GeometricAlbedo { get; set; }
        public string? MassRatioToSun { get; set; }
        public string? MeanTemperature { get; set; }
        public string? AtmosphericPressure { get; set; }
        public string? ObliquityToOrbit { get; set; }
        public string? MaxAngularDiameter { get; set; }
        public string? MeanSideRealOrbitalPeriod { get; set; }
        public string? OrbitalSpeed { get; set; }
        public string? HillsSphereRadius { get; set; }
        public string? EscapeSpeed { get; set; }
        public string? GravitationalParameter { get; set; }
        public string? SolarConstantPerihelion { get; set; }
        public string? SolarConstantAphelion { get; set; }
        public string? SolarConstantMean { get; set; }
        public string? MaxPlanetaryIRPerihelion { get; set; }
        public string? MaxPlanetaryIRAphelion { get; set; }
        public string? MaxPlanetaryIRMean { get; set; }
        public string? MinPlanetaryIRPerihelion { get; set; }
        public string? MinPlanetaryIRAphelion { get; set; }
        public string? MinPlanetaryIRMean { get; set; }

        public static PlanetMongoDTO ConvertToMongoDTO(Planet planet)
        {
            return new PlanetMongoDTO
            {
                HorizonId = planet.HorizonId,
                Name = planet.Name,
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