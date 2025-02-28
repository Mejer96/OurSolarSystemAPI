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
            };
        }




        public async Task<List<IRecord>> createMoonNodeFromMoonObject(Moon moon, int planetHorizonId, int barycenterHorizonId)
        {
            await using var session = _driver.AsyncSession();
            Dictionary<string, object?> moonAttributes = ConvertMoonAttributesToDict(moon);



            var parameters = new Dictionary<string, object>
            {
                {"moon", moonAttributes},
                {"planetId", planetHorizonId},
                {"barycenterId", barycenterHorizonId}
            };

            var query = @"
                CREATE (m:Moon $moon)
                WITH m
                MATCH (b:Barycenter) WHERE b.horizonId = $barycenterId
                CREATE (m)-[:ORBITS]->(b)
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