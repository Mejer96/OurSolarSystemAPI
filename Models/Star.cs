using System.Text.Json.Serialization;

namespace OurSolarSystemAPI.Models
{
    public class Star
    {
        public int Id { get; set; }
        public int SolarSystemBarycenterId { get; set;}
        public int SolarSystemBarycenterHorizonId { get; set;}
        public SolarSystemBarycenter SolarSystemBarycenter { get; set; }
        public int HorizonId { get; set; }
        public List<EphemerisSun>? Ephemeris { get; set; }
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
 
    }
}
