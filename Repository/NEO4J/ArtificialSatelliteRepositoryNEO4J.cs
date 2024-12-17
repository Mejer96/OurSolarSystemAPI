using OurSolarSystemAPI.Models;
using Neo4j.Driver;
using Neo4j.Driver.Mapping;

namespace OurSolarSystemAPI.Repository.NEO4J 
{
    public class ArtificialSatelliteRepositoryNEO4J 
    {
        private readonly IDriver _driver;

        public ArtificialSatelliteRepositoryNEO4J(IDriver driver)
        {
            _driver = driver;
        }

        public Dictionary<string, object?> ConvertSatelliteAttributesToDict(ArtificialSatellite satellite)
        {
            var satelliteDict = new Dictionary<string, object?>();

            satelliteDict["launchDate"] = satellite.LaunchDate;
            satelliteDict["launchSite"] = satellite.LaunchSite;
            satelliteDict["bStarDragTerm"] = satellite.BStarDragTerm;
            satelliteDict["eccentricity"] = satellite.Eccentricity;
            satelliteDict["meanAnomaly"] = satellite.MeanAnomaly;
            satelliteDict["orbitNumber"] = satellite.OrbitNumber;
            satelliteDict["source"] = satellite.Source;
            satelliteDict["noradId"] = satellite.NoradId;
            satelliteDict["nssdcId"] = satellite.NssdcId;
            satelliteDict["perigee"] = satellite.Perigee;
            satelliteDict["apogee"] = satellite.Apogee;
            satelliteDict["inclination"] = satellite.Inclination;
            satelliteDict["period"] = satellite.Period;
            satelliteDict["semiMajorAxis"] = satellite.SemiMajorAxis;
            satelliteDict["rcs"] = satellite.Rcs;
            satelliteDict["name"] = satellite.Name;

            return satelliteDict;
        }

        public Dictionary<string, object?> ConvertSatelliteOrbitalAttributesToDict(ArtificialSatellite satellite)
        {
            var satelliteDict = new Dictionary<string, object?>();

            satelliteDict["bStarDragTerm"] = satellite.BStarDragTerm;
            satelliteDict["eccentricity"] = satellite.Eccentricity;
            satelliteDict["meanAnomaly"] = satellite.MeanAnomaly;
            satelliteDict["orbitNumber"] = satellite.OrbitNumber;
            satelliteDict["perigee"] = satellite.Perigee;
            satelliteDict["apogee"] = satellite.Apogee;
            satelliteDict["inclination"] = satellite.Inclination;
            satelliteDict["period"] = satellite.Period;
            satelliteDict["semiMajorAxis"] = satellite.SemiMajorAxis;

            return satelliteDict;
        }

        public async Task<List<IRecord>> createSatelliteNodeFromObject(ArtificialSatellite satellite, int celestialBodyHorizonId) 
        {
            await using var session = _driver.AsyncSession();
            Dictionary<string, object?> satelliteAttributes = ConvertSatelliteAttributesToDict(satellite);
            Dictionary<string, object?> orbitAttributes = ConvertSatelliteOrbitalAttributesToDict(satellite);
            
            var parameters = new Dictionary<string, object> 
            {
                {"satellite", satelliteAttributes},
                {"orbit", orbitAttributes},
                {"celestialBodyId", celestialBodyHorizonId}
            };

            var query = @"
                CREATE (s:Satellite $satellite)
                WITH s
                MATCH (p:Planet) WHERE p.horizonId = $celestialBodyId
                CREATE (s)-[:ORBITS $orbit]->(p)
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