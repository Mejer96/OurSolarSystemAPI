using Neo4j.Driver;
using Neo4j.Driver.Mapping;
using OurSolarSystemAPI.Models;


namespace OurSolarSystemAPI.Repository.NEO4J
{

    public class BarycenterRepositoryNEO4J
    {
        private readonly IDriver _driver;

        public BarycenterRepositoryNEO4J(IDriver driver)
        {
            _driver = driver;
        }

        public Dictionary<string, object> ConvertBarycenterObjectToDict(Barycenter barycenter)
        {
            return new Dictionary<string, object>
            {
                {"name", barycenter.Name},
                {"horizonId", barycenter.HorizonId}
            };
        }

        public async Task CreateSolarSystemBarycenterNode(Barycenter solarSystemBarycenter)
        {
            await using var session = _driver.AsyncSession();
            Dictionary<string, object> barycenter = ConvertBarycenterObjectToDict(solarSystemBarycenter);
            var parameters = new Dictionary<string, object>
            {
                { "barycenter", barycenter }
            };

            var query = "CREATE (b:Barycenter $barycenter) RETURN b";
            var result = await session.ExecuteWriteAsync(async tx =>
                {
                    var cursor = await tx.RunAsync(query, parameters);
                    return await cursor.SingleAsync();
                });


        }



        public async Task<List<IRecord>> CreateBarycenterNodeFromObject(Barycenter barycenter)
        {
            await using var session = _driver.AsyncSession();
            Dictionary<string, object> barycenterDict = ConvertBarycenterObjectToDict(barycenter);

            var parameters = new Dictionary<string, object>
            {
                {"barycenter", barycenterDict},
                {"solarSystemBarycenterId", 0}
            };

            var query = @"
                CREATE (b:Barycenter $barycenter)
                WITH b
                MATCH (ssb:Barycenter) WHERE ssb.horizonId = $solarSystemBarycenterId
                CREATE (b)-[:ORBITS]->(ssb)
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