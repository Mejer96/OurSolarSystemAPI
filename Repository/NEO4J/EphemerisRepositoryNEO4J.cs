using Neo4j.Driver;
using Neo4j.Driver.Mapping;
using OurSolarSystemAPI.Models;

namespace OurSolarSystemAPI.Repository.NEO4J 
{
    public class EphemerisRepositoryNEO4J 
    {
        private readonly IDriver _driver;

        public EphemerisRepositoryNEO4J(IDriver driver)
        {
            _driver = driver;
        }

        public Dictionary<string, object> ConvertTleObjectToDict(TleArtificialSatellite tle) 
        {
            return new Dictionary<string, object> 
            {
                {"noradId", tle.NoradId},
                {"firstLine", tle.TleFirstLine},
                {"secondLine", tle.TleSecondLine},
                {"scrapedAt", tle.ScrapedAt}
            };
        }

        public async Task<List<IRecord>> createEphimerisNodesFromList(IEnumerable<Ephemeris> ephemeris, int horizonId, string relatedNode) 
        {
            await using var session = _driver.AsyncSession();
            var ephemerisDict = new List<Dictionary<string, object>>();

            foreach(var entry in ephemeris) 
            {
                ephemerisDict.Add(entry.ConvertObjectToDictNEO4J());
            }

            var parameters = new Dictionary<string, object> 
            {
                {"ephemerisData", ephemerisDict},
                {"horizonId", horizonId}
            };

            var query = @$"
                UNWIND $ephemerisData AS Ephemeris
                CREATE (e:Ephemeris)
                SET e = Ephemeris
                WITH e
                MATCH (x:{relatedNode}) WHERE x.horizonId = $horizonId
                CREATE (x)-[:HAS_LOCATION]->(e)
                RETURN count(*) AS count";

                var result = await session.ExecuteWriteAsync(async tx =>
                {
                    var cursor = await tx.RunAsync(query, parameters);
                    return await cursor.ToListAsync();
                });
            return result;
        }

        public async Task<List<IRecord>> createTLENodesFromList(List<TleArtificialSatellite> tles, int noradId) 
        {
            await using var session = _driver.AsyncSession();
            var tlesDict = new List<Dictionary<string, object>>();

            foreach(var entry in tles) 
            {
                tlesDict.Add(ConvertTleObjectToDict(entry));
            }

            var parameters = new Dictionary<string, object> 
            {
                {"tles", tlesDict},
                {"noradId", noradId}
            };

            var query = @"
                UNWIND $tles AS Tle
                CREATE (t:Tle)
                SET t = Tle
                WITH t
                MATCH (s:Satellite) WHERE s.noradId = $noradId
                CREATE (s)-[:HAS_TLE]->(t)
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