using Microsoft.EntityFrameworkCore;
using OurSolarSystemAPI.Models;
using OurSolarSystemAPI.Repository.MySQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarSystemTest
{
    [TestClass]
    public class PlanetintegrationtestGet
    {
        private PlanetRepositoryMySQL _repository;
        private OurSolarSystemContext _context;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<OurSolarSystemContext>()
                .UseMySql("Server=localhost;Port=3307;Database=OurSolarSystem2.0;Uid=root;Pwd=Kea2024vinter!;",
                          new MySqlServerVersion(new Version(8, 0, 0)))
                .Options;

            _context = new OurSolarSystemContext(options);
            _repository = new PlanetRepositoryMySQL(_context);

            _context.Database.EnsureCreated(); // Opret nødvendige tabeller
        }

        [TestMethod]
        public async Task GetByHorizonId_ShouldReturnPlanetWithEphemerisAndMoons()
        {
            var solarSystemBarycenter = new SolarSystemBarycenter
            {
                Id = 65, // Unikt ID for denne test
                Name = "TestSolarSystemBarycenter"
            };

            _context.SolarSystemBaryCenter.Add(solarSystemBarycenter);
            await _context.SaveChangesAsync();

            var barycenter = new Barycenter
            {
                Id = 63, // Unikt ID for barycenter
                Name = "TestBarycenter",
                HorizonId = 1234, // Unikt HorizonId
                SolarSystemBarycenterId = solarSystemBarycenter.Id // Referer til SolarSystemBarycenter

            };

            _context.Barycenters.Add(barycenter);
            await _context.SaveChangesAsync();

            // Arrange: Tilføj testdata
            var planet = new Planet
            {
                HorizonId = 206, // Unikt ID for denne test
                Name = "PlanetWithMoonsAndEphemeris",
                BarycenterId = barycenter.Id,
                Ephemeris = new List<EphemerisPlanet>
        {
            new EphemerisPlanet
            {
                JulianDate = 2451545.0, // Initialiser JulianDate
                DateTime = new DateTime(2025, 1, 1), // Initialiser DateTime
                PositionX = 1.0,
                PositionY = 2.0,
                PositionZ = 3.0,
                VelocityX = 0.5,
                VelocityY = 0.4,
                VelocityZ = 0.3
            }
        },
                Moons = new List<Moon>
        {
            new Moon { HorizonId = 123, Name = "Moon1" , BarycenterId = barycenter.Id},
            new Moon { HorizonId = 156,Name = "Moon2", BarycenterId = barycenter.Id}
        }
            };

            _context.Planets.Add(planet);
            await _context.SaveChangesAsync();

            // Act: Hent planeten via repository
            var retrievedPlanet = await _repository.GetLocationsByHorizonId(206);

            // Assert: Validér resultatet
            Assert.IsNotNull(retrievedPlanet);
            Assert.AreEqual("PlanetWithMoonsAndEphemeris", retrievedPlanet.Name);
            Assert.AreEqual(2, retrievedPlanet.Moons.Count);
            Assert.AreEqual(1, retrievedPlanet.Ephemeris.Count);
            Assert.AreEqual("Moon1", retrievedPlanet.Moons[0].Name);
            Assert.AreEqual(1.0, retrievedPlanet.Ephemeris[0].PositionX);
            Assert.AreEqual(0.5, retrievedPlanet.Ephemeris[0].VelocityX);
        }


        [TestCleanup]
        public void Teardown()
        {
            // Slet kun de data, der blev oprettet i testen
            _context.Planets.RemoveRange(_context.Planets);
            _context.Moons.RemoveRange(_context.Moons);
            _context.Barycenters.RemoveRange(_context.Barycenters);
            _context.SolarSystemBaryCenter.RemoveRange(_context.SolarSystemBaryCenter);

            _context.SaveChanges();
            _context.Dispose();
        }
    }
}
