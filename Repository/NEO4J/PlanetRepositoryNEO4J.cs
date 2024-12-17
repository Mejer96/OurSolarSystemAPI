using Neo4j.Driver;
using Neo4j.Driver.Mapping;
using OurSolarSystemAPI.Models;

namespace OurSolarSystemAPI.Repository.NEO4J 
{
    public class PlanetRepositoryNEO4J 
    {
        private readonly IDriver _driver;

        public PlanetRepositoryNEO4J(IDriver driver)
        {
            _driver = driver;
        }

        public Dictionary<string, object?> ConvertPlanetAttributesToDict(Planet planet)
        {
            return new Dictionary<string, object?>
            {
                { "horizonId", planet.HorizonId },
                { "name", planet.Name },
                { "volumeMeanRadius", planet.VolumeMeanRadius },
                { "density", planet.Density },
                { "mass", planet.Mass },
                { "volume", planet.Volume },
                { "equatorialRadius", planet.EquatorialRadius },
                { "siderealRotationPeriod", planet.SiderealRotationPeriod },
                { "siderealRotationRate", planet.SiderealRotationRate },
                { "meanSolarDay", planet.MeanSolarDay },
                { "polarGravity", planet.PolarGravity },
                { "equatorialGravity", planet.EquatorialGravity },
                { "geometricAlbedo", planet.GeometricAlbedo },
                { "massRatioToSun", planet.MassRatioToSun },
                { "meanTemperature", planet.MeanTemperature },
                { "atmosphericPressure", planet.AtmosphericPressure },
                { "maxAngularDiameter", planet.MaxAngularDiameter },
                { "hillsSphereRadius", planet.HillsSphereRadius },
                { "escapeSpeed", planet.EscapeSpeed },
                { "gravitationalParameter", planet.GravitationalParameter },
                { "maxPlanetaryIRPerihelion", planet.MaxPlanetaryIRPerihelion },
                { "maxPlanetaryIRAphelion", planet.MaxPlanetaryIRAphelion },
                { "maxPlanetaryIRMean", planet.MaxPlanetaryIRMean },
                { "minPlanetaryIRPerihelion", planet.MinPlanetaryIRPerihelion },
                { "minPlanetaryIRAphelion", planet.MinPlanetaryIRAphelion },
                { "minPlanetaryIRMean", planet.MinPlanetaryIRMean }
            };
        }


        public Dictionary<string, object?> ConvertPlanetOrbitalAttributesToDict(Planet planet)
        {
            return new Dictionary<string, object?>
            {
                { "obliquityToOrbit", planet.ObliquityToOrbit },
                { "meanSideRealOrbitalPeriod", planet.MeanSideRealOrbitalPeriod },
                { "orbitalSpeed", planet.OrbitalSpeed },
                { "solarConstantPerihelion", planet.SolarConstantPerihelion },
                { "solarConstantAphelion", planet.SolarConstantAphelion },
                { "solarConstantMean", planet.SolarConstantMean }
            };
        }
        public async Task<IRecord> CreatePlanetNode(Planet planet)
        {
            await using var session = _driver.AsyncSession();

            Dictionary<string, object?> planetAttributes = ConvertPlanetAttributesToDict(planet);
            Dictionary<string, object?> orbitAttributes = ConvertPlanetOrbitalAttributesToDict(planet);

            var parameters = new Dictionary<string, object>
            {
                {"planet", planetAttributes},
                {"orbit", orbitAttributes},
                {"barycenterHorizonId", planet.BarycenterHorizonId}
            };

            var query = @"
                CREATE (p:Planet $planet)
                WITH p
                MATCH (b:Barycenter) WHERE b.horizonId = $barycenterHorizonId
                CREATE (p)-[:ORBITS $orbit]->(b)
                RETURN p";

            var result = await session.ExecuteWriteAsync(async tx =>
            {
                var cursor = await tx.RunAsync(query, parameters);
                return await cursor.SingleAsync();
            });

            return result;
        }
    }
    
}