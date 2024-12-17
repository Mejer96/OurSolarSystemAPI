using Neo4j.Driver;
using Neo4j.Driver.Mapping;
using OurSolarSystemAPI.Models;

namespace OurSolarSystemAPI.Repository.NEO4J 
{
    public class MoonRepositoryNEO4J 
    {
        private readonly IDriver _driver;

        public MoonRepositoryNEO4J(IDriver driver)
        {
            _driver = driver;
        }

        public Dictionary<string, object?> ConvertMoonAttributesToDict(Moon moon)
        {
            return new Dictionary<string, object?>
            {
                { "horizonId", moon.HorizonId },
                { "name", moon.Name },
                { "meanRadius", moon.MeanRadius },
                { "density", moon.Density },
                { "gm", moon.Gm },
                { "semiMajorAxis", moon.SemiMajorAxis },
                { "gravitationalParameter", moon.GravitationalParameter },
                { "geometricAlbedo", moon.GeometricAlbedo },
                { "orbitalPeriod", moon.OrbitalPeriod },
                { "eccentricity", moon.Eccentricity },
                { "rotationalPeriod", moon.RotationalPeriod },
                { "inclination", moon.Inclination }
            };
        }

        public Dictionary<string, object?> ConvertMoonOrbitalAttributesToDict(Moon moon)
        {
            return new Dictionary<string, object?>
            {
                { "semiMajorAxis", moon.SemiMajorAxis },
                { "gravitationalParameter", moon.GravitationalParameter },
                { "geometricAlbedo", moon.GeometricAlbedo },
                { "orbitalPeriod", moon.OrbitalPeriod },
                { "eccentricity", moon.Eccentricity },
                { "rotationalPeriod", moon.RotationalPeriod },
                { "inclination", moon.Inclination }
            };
        }


        public async Task<List<IRecord>> createMoonNodeFromMoonObject(Moon moon, int planetHorizonId, int barycenterHorizonId) 
        {
            await using var session = _driver.AsyncSession();
            Dictionary<string, object?> moonAttributes = ConvertMoonAttributesToDict(moon);
            Dictionary<string, object?> orbitAttributes = ConvertMoonOrbitalAttributesToDict(moon);

            
            var parameters = new Dictionary<string, object> 
            {
                {"moon", moonAttributes},
                {"orbit", orbitAttributes},
                {"planetId", planetHorizonId},
                {"barycenterId", barycenterHorizonId}
            };

            var query = @"
                CREATE (m:Moon $moon)
                WITH m
                MATCH (b:Barycenter) WHERE b.horizonId = $barycenterId
                CREATE (m)-[:ORBITS $orbit]->(b)
                WITH m
                MATCH (p:Planet) WHERE p.horizonId = $planetId
                CREATE (m)-[:PART_OF_MOON_SYSTEM_OF]->(p)
                RETURN count(*) AS count";

                var result = await session.ExecuteWriteAsync(async tx =>
                {
                    var cursor = await tx.RunAsync(query, parameters);
                    return await cursor.ToListAsync();
                });
            return result;
        }
    }
    
}