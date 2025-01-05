using Microsoft.AspNetCore.Mvc;
using OurSolarSystemAPI.Service.MongoDB;

namespace OurSolarSystemAPI.Controllers.NEO4J; 

    [ApiController]
    [Route("migration/mongodb")]
    public class MigrationControllerMongoDB : ControllerBase
    {

        private readonly MigrationServiceMongoDB _migrationService;
       
        public MigrationControllerMongoDB(MigrationServiceMongoDB migrationService) 
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

        [HttpGet("migrate-sun")]
        public async Task<IActionResult> ScrapeSun() 
        {
            await _migrationService.MigrateSun();

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