using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using OurSolarSystemAPI.Models;

namespace OurSolarSystemAPI.Repository.MongoDB
{
    public class StarMongoDTO
    {
        [BsonId]
        public ObjectId Id { get; set; }
 
        public int HorizonId { get; set; }
        public double GravitationalParameter { get; set; }

        public double Mass { get; set; }

        public double VolumeMeanRadius { get; set; }

        public double Volume { get; set; }

        public double SolarRadius { get; set; }
        public double Radius { get; set; }

        public double AngularDiameter { get; set; }

        public double PhotosphereTemperatureTop { get; set; }
        public double PhotosphereTemperatureBottom { get; set; }

        public double PhotosphericDepth { get; set; }

        public double ChromosphericDepth { get; set; }

        public double Flatness { get; set; }

        public double SurfaceGravity { get; set; }

        public double EscapeSpeed { get; set; }

        public double RightAscension { get; set; }

        public double Declination { get; set; }

        public double ObliquityToEcliptic { get; set; }

        public double SolarConstantOneAu { get; set; }

        public double Luminosity { get; set; }

        public double MassEnergyConversionRate { get; set; }

        public double EffectiveTemperature { get; set; }

        public double SunspotCycle { get; set; }

        public double Cycle24SunspotMinimum { get; set; }

        public static StarMongoDTO ConvertToStarMongoDTO(Star star)
        {
            return new StarMongoDTO
            {
                HorizonId = star.HorizonId,
                GravitationalParameter = star.GravitationalParameter,
                Mass = star.Mass,
                VolumeMeanRadius = star.VolumeMeanRadius,
                Volume = star.Volume,
                SolarRadius = star.SolarRadius,
                Radius = star.Radius,
                AngularDiameter = star.AngularDiameter,
                PhotosphereTemperatureTop = star.PhotosphereTemperatureTop,
                PhotosphereTemperatureBottom = star.PhotosphereTemperatureBottom,
                PhotosphericDepth = star.PhotosphericDepth,
                ChromosphericDepth = star.ChromosphericDepth,
                Flatness = star.Flatness,
                SurfaceGravity = star.SurfaceGravity,
                EscapeSpeed = star.EscapeSpeed,
                RightAscension = star.RightAscension,
                Declination = star.Declination,
                ObliquityToEcliptic = star.ObliquityToEcliptic,
                SolarConstantOneAu = star.SolarConstantOneAu,
                Luminosity = star.Luminosity,
                MassEnergyConversionRate = star.MassEnergyConversionRate,
                EffectiveTemperature = star.EffectiveTemperature,
                SunspotCycle = star.SunspotCycle,
                Cycle24SunspotMinimum = star.Cycle24SunspotMinimum
            };
        }

 
    }
}
