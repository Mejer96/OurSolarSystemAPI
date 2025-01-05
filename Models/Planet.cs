using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OurSolarSystemAPI.Models
{
    public class Planet
    {
        public int Id { get; set; }
        public required int HorizonId { get; set; }
        public int BarycenterId { get; set; }
        public Barycenter Barycenter { get; set; }
        public int BarycenterHorizonId { get; set; }
        public List<Moon>? Moons { get; set; }
        public List<EphemerisPlanet> Ephemeris { get; set; }
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
    }

    

}
