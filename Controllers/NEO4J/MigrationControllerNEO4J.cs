using Microsoft.AspNetCore.Mvc;
using OurSolarSystemAPI.Service.NEO4J;

namespace OurSolarSystemAPI.Controllers.NEO4J; 

    [ApiController]
    [Route("migration/neo4j")]
    public class MigrationControllerNEO4J : ControllerBase
    {

        private readonly MigrationServiceNEO4J _migrationService;
       
        public MigrationControllerNEO4J(MigrationServiceNEO4J migrationService) 
        {
            _migrationService = migrationService;
        }


        [HttpGet("migrate-barycenters")]
        public async Task<IActionResult> ScrapeBarycenters() 
        {
            await _migrationService.MigrateBarycenters();

            return Ok(new
            {
                statusCode = 200
            });
        }

        [HttpGet("migrate-planets")]
        public async Task<IActionResult> ScrapePlanets() 
        {
            await _migrationService.MigratePlanets();

            return Ok(new
            {
                statusCode = 200
            });
 
        }

        [HttpGet("migrate-moons")]
        public async Task<IActionResult> ScrapeMoons() 
        {
            await _migrationService.MigrateMoons();

            return Ok(new
            {
                statusCode = 200
            });
 
        }

        [HttpGet("migrate-satellites")]
        public async Task<IActionResult> ScrapeSatelittes() 
        {
            await _migrationService.MigrateArtificialSatellites();

            return Ok(new
            {
                statusCode = 200
            });
 
        }

}
    

