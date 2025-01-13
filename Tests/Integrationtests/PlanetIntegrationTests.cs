using Microsoft.EntityFrameworkCore;
using OurSolarSystemAPI.Models;
using OurSolarSystemAPI.Repository.MySQL;
using System.Numerics;

namespace SolarSystemTest
{
    [TestClass]
    public class PlanetIntegrationTests
    {
        private PlanetRepositoryMySQL _repository;
        private OurSolarSystemContext _context;
        private Planet _testPlanet;


        [TestInitialize]
        public void Setup()
        {
            // Opret DbContext med forbindelse til den originale database
            var options = new DbContextOptionsBuilder<OurSolarSystemContext>()
                .UseMySql("Server=localhost;Port=3307;Database=OurSolarSystem2.0;Uid=root;Pwd=Kea2024vinter!;",
                          new MySqlServerVersion(new Version(8, 0, 0)))
                .Options;

            _context = new OurSolarSystemContext(options);
            _repository = new PlanetRepositoryMySQL(_context);

            var solarSystemBarycenter = new SolarSystemBarycenter
            {
                Id = 346, // Sørg for, at dette ID er unikt
                Name = "TestSolarSystemBarycenter",
                HorizonId = 12345
            };
            _context.SolarSystemBaryCenter.Add(solarSystemBarycenter);
            _context.SaveChanges();
            var barycenter = new Barycenter
            {
                Id = 10, 
                Name = "TestBarycenter",
                HorizonId = 10,
                SolarSystemBarycenterId = solarSystemBarycenter.Id // Referer til SolarSystemBarycenter

            };
            _context.Barycenters.Add(barycenter);
            _context.SaveChanges();

            // Tilføj testdata
            _testPlanet = new Planet
            {
                HorizonId = 7655, // Unikt ID for at undgå konflikter
                Name = "TestPlanet",
                Mass = 5.972e24,
                EquatorialRadius = 6371,
                BarycenterId = barycenter.Id 

            };

            _context.Planets.Add(_testPlanet);
            _context.SaveChanges();
        }

        [TestMethod]
        public async Task AddPlanet_ShouldInsertPlanetIntoDatabase()
        {
            // Act: Hent planeten fra databasen
            var retrievedPlanet = await _repository.GetByHorizonId(7655);

            // Assert: Validér, at planeten er korrekt oprettet
            Assert.IsNotNull(retrievedPlanet);
            Assert.AreEqual("TestPlanet", retrievedPlanet.Name);
            Assert.AreEqual(5.972e24, retrievedPlanet.Mass);
            Assert.AreEqual(6371, retrievedPlanet.EquatorialRadius);
        }

        [TestCleanup]
        public void Teardown()
        {
            var planetToDelete = _context.Planets.FirstOrDefault(p => p.HorizonId == 101);
            if (planetToDelete != null)
            {
                _context.Planets.Remove(planetToDelete);
            }

            // Fjern testbarycenter
            var barycenterToDelete = _context.Barycenters.FirstOrDefault(b => b.Name == "TestBarycenter");
            if (barycenterToDelete != null)
            {
                _context.Barycenters.Remove(barycenterToDelete);
            }

            // Fjern testSolarSystemBarycenter
            var solarSystemBarycenterToDelete = _context.SolarSystemBaryCenter.FirstOrDefault(s => s.Name == "TestSolarSystemBarycenter");
            if (solarSystemBarycenterToDelete != null)
            {
                _context.SolarSystemBaryCenter.Remove(solarSystemBarycenterToDelete);
            }

            _context.SaveChanges();
            _context.Dispose();
        }

    }
}