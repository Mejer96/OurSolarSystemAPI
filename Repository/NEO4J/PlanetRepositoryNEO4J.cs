using Neo4j.Driver;
using Neo4j.Driver.Mapping;
using OurSolarSystemAPI.Models;
using System.Text.Json; // Or Newtonsoft.Json

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

        public async Task<string> FetchEphemerisByHorizonIdAndDate(int horizonId, DateTime date)
        {
            const string query = @"
                MATCH (p:Planet {horizonId: 399})-[:HAS_LOCATION]->(e:Ephemeris) 
                WHERE e.DateTime.year = 2024 AND e.DateTime.month = 1 AND e.DateTime.day = 1
                RETURN p as planet, collect(e) as ephemeris";

            await using var session = _driver.AsyncSession();

            try
            {
                var result = await session.ExecuteReadAsync(async tx =>
                {
                    var cursor = await tx.RunAsync(query, new { horizonId, date = date.ToString("yyyy-MM-dd") });
                    var record = await cursor.SingleAsync(); // Avoid exceptions if no match

                    var properties = record?["e"].As<INode>()?.Properties;

                    if (properties != null)
                    {
                     
                        return JsonSerializer.Serialize(properties); 
                        
                    }

                    return null;
                });

                return result;
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<string> FetchAllEphemerisByHorizonId(int horizonId)
        {
            const string query = @"
                MATCH (p:Planet {horizonId: $horizonId})-[:HAS_LOCATION]->(e:Ephemeris)
                RETURN e";

            await using var session = _driver.AsyncSession();

            try
            {
                var result = await session.ExecuteReadAsync(async tx =>
                {
                    var cursor = await tx.RunAsync(query, new { horizonId });
                    var nodes = await cursor.ToListAsync(record =>
                    {
                        var node = record["e"].As<INode>();
                        return node.Properties;
                    });

                    // Convert the list of properties to a JSON array
                    return JsonSerializer.Serialize(nodes); // Or JsonConvert.SerializeObject(nodes) for Newtonsoft.Json
                });

                return result;
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<string> FetchEphemerisByNameAndDate(string name, DateTime date)
        {
            const string query = @"
                MATCH (p:Planet {name: $name})-[:HAS_LOCATION]->(e:Ephemeris) 
                WHERE e.DateTime.year = 2024 AND e.DateTime.month = 1 AND e.DateTime.day = 1
                RETURN p as planet, collect(e) as ephemeris";

            await using var session = _driver.AsyncSession();

            try
            {
                var result = await session.ExecuteReadAsync(async tx =>
                {
                    var cursor = await tx.RunAsync(query, new { name, date = date.ToString("yyyy-MM-dd") });
                    var record = await cursor.SingleAsync(); 

                    var properties = record?["e"].As<INode>()?.Properties;

                    if (properties != null)
                    {
                     
                        return JsonSerializer.Serialize(properties); 
                        
                    }

                    return null; 
                });

                return result;
            }
            finally
            {
                await session.CloseAsync();
            }
        }
    }
    
}