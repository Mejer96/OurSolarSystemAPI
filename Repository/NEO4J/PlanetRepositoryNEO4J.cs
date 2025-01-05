using Neo4j.Driver;
using Neo4j.Driver.Mapping;
using OurSolarSystemAPI.Models;
using System.Text.Json;

namespace OurSolarSystemAPI.Repository.NEO4J 
{
    public class PlanetRepositoryNEO4J 
    {
        private readonly IDriver _driver;
        private readonly JsonSerializerOptions SerializerOptions = new() { WriteIndented = true };

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

        public async Task<string> GetLocationByHorizonIdAndDate(int horizonId, DateTime date)
        {
            const string query = @"
                MATCH (p:Planet {horizonId: $horizonId})-[:HAS_LOCATION]->(e:Ephemeris) 
                WHERE e.DateTime.year = $year AND e.DateTime.month = $month AND e.DateTime.day = $day
                RETURN p as planet, e as ephemeris";

            await using var session = _driver.AsyncSession();

            try
            {
                var result = await session.ExecuteReadAsync(async tx =>
                {
                    var parameters = new
                    {
                        horizonId,
                        year = date.Year,
                        month = date.Month,
                        day = date.Day
                    };
                    var cursor = await tx.RunAsync(query, parameters);
                    var record = await cursor.SingleAsync();

                    var properties = record?["ephemeris"].As<INode>()?.Properties;

                    if (properties != null)
                    {
                        
                        return JsonSerializer.Serialize(properties, SerializerOptions); 
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

        public async Task<string> GetAttribute(int horizonId, string attribute)
        {
            const string query = @"
                MATCH (p:Planet {horizonId: $horizonId}) 
                RETURN p[$attribute] as attribute";

            await using var session = _driver.AsyncSession();

            try
            {
                var result = await session.ExecuteReadAsync(async tx =>
                {
                    var parameters = new
                    {
                        horizonId,
                        attribute
                    };
                    var cursor = await tx.RunAsync(query, parameters);
                    var record = await cursor.SingleAsync();

                    var properties = record?["attribute"].As<INode>()?.Properties;

                    if (properties != null)
                    {
                        
                        return JsonSerializer.Serialize(properties, SerializerOptions); 
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

        public async Task<string> GetLocationsByHorizonId(int horizonId)
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
                    return JsonSerializer.Serialize(nodes);
                });

                return result;
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<string> GetDistanceBetween(int firstHorizonId, int secondHorizonId, DateTime date)
        {
            const string query = @"
            MATCH (p1:Planet {horizonId: $firstHorizonId})-[:HAS_LOCATION]->(e1:Ephemeris),
            (p2:Planet {horizonId: $secondHorizonId})-[:HAS_LOCATION]->(e2:Ephemeris)
            WHERE e1.DateTime.year = $year AND e1.DateTime.month = $month AND e1.DateTime.day = $day
            AND e2.DateTime.year = $year AND e2.DateTime.month = $month AND e2.DateTime.day = $day
            WITH e1, e2,
                sqrt(
                    (e1.positionX - e2.positionX)^2 +
                    (e1.positionY - e2.positionY)^2 +
                    (e1.positionZ - e2.positionZ)^2
                ) AS distance
            RETURN e1.DateTime AS dateTime, distance";

            await using var session = _driver.AsyncSession();

            try
            {
                var result = await session.ExecuteReadAsync(async tx =>
                {
                    var parameters = new
                    {
                        firstHorizonId,
                        secondHorizonId,
                        year = date.Year,
                        month = date.Month,
                        day = date.Day
                    };
                    var cursor = await tx.RunAsync(query, parameters);
                    var nodes = await cursor.ToListAsync(record =>
                    {
                        var dateTime = record["dateTime"].As<LocalDateTime>().ToString();
                        var distance = record["distance"].As<double>();
                        return new { dateTime, distance };
                    });

                    return JsonSerializer.Serialize(nodes, SerializerOptions);
                });

                return result;
            }
            finally
            {
                await session.CloseAsync();
            }
        }


        public async Task<string> GetByName(string name)
        {
            const string query = @"
                MATCH (p:Planet {name: $name}) 
                RETURN p as planet";

            await using var session = _driver.AsyncSession();

            try
            {
                var result = await session.ExecuteReadAsync(async tx =>
                {
                    var parameters = new
                    {
                        name
                    };
                    var cursor = await tx.RunAsync(query, parameters);
                    var record = await cursor.SingleAsync(); 

                    var properties = record?["planet"].As<INode>()?.Properties;

                    if (properties != null)
                    {
                        return JsonSerializer.Serialize(properties, SerializerOptions);    
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

        public async Task<string> GetByHorizonId(int horizonId)
        {
            const string query = @"
                MATCH (p:Planet {name: $horizonId}) 
                RETURN p as planet";

            await using var session = _driver.AsyncSession();

            try
            {
                var result = await session.ExecuteReadAsync(async tx =>
                {
                    var parameters = new
                    {
                        horizonId
                    };
                    var cursor = await tx.RunAsync(query, parameters);
                    var record = await cursor.SingleAsync(); 

                    var properties = record?["planet"].As<INode>()?.Properties;

                    if (properties != null)
                    {
                        return JsonSerializer.Serialize(properties, SerializerOptions);    
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