using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using OurSolarSystemAPI.Models;

namespace OurSolarSystemAPI.Repository.MySQL 
{

    public class PlanetRepositoryMySQL 
    {

        private readonly OurSolarSystemContext _context;

        public PlanetRepositoryMySQL(OurSolarSystemContext context) 
        {
            _context = context;
        }

        public void CreatePlanet(Planet planet) 
        {
            _context.Planets.Add(planet);
            _context.SaveChanges();
        }
        
        public void CreateSun(Star sun) 
        {
            _context.Sun.Add(sun);
            _context.SaveChanges();
        }

        public async Task<Star> RequestSunWithEphemeris() 
        {
            return await _context.Sun
            .Include(s => s.Ephemeris)
            .FirstOrDefaultAsync(s => s.HorizonId == 10);
        }

        public async Task<List<Planet>> requestAllPlanets() 
        {
            return await _context.Planets
            .ToListAsync();
        }

        public async Task<List<Planet>> requestAllPlanetsWithMoons() 
        {
            return await _context.Planets
            .Include(p => p.Moons)
            .ToListAsync();
        }
        public async Task<List<Planet>> requestAllPlanetsWithEphemeris() 
        {
            return await _context.Planets
            .Include(p => p.Ephemeris)
            .ToListAsync();
        }

        public async Task<List<Planet>> requestAllPlanetsWithEphemerisAndMoons() 
        {
            return await _context.Planets
            .Include(p => p.Ephemeris)
            .Include(p => p.Moons)
            .ToListAsync();
        }

        public async Task<List<Planet>> requestAllPlanetsWithEphemerisAndMoonsWithEphemeris() 
        {
            return await _context.Planets
                .Include(p => p.Ephemeris)  // Include Ephemeris of the Planet
                .Include(p => p.Moons)      // Include Moons navigation property
                    .ThenInclude(m => m.Ephemeris)  // Include Ephemeris for each Moon
                .ToListAsync();
        }

        public Planet? RequestPlanetById(int horizonId) 
        {
            return _context.Planets.FirstOrDefault(p => p.HorizonId == horizonId);
        }

        public Planet? RequestPlanetLocationByNameAndDateTime(string name, DateTime dateTime)
        {
            return _context.Planets
                .Where(p => p.Name == name)
                .Include(p => p.Ephemeris.Where(e => e.DateTime.Date == dateTime.Date))
                .FirstOrDefault();
        }

        public Planet? RequestPlanetLocationByHorizonId(int horizonId)
        {
            return _context.Planets
                .Where(p => p.HorizonId == horizonId)
                .Include(p => p.Ephemeris)
                .FirstOrDefault();
        }

        public Planet? RequestPlanetByName(string planetName) 
        {
            return _context.Planets.FirstOrDefault(p => p.Name == planetName);
        }

        public async Task<List<EphemerisPlanet>> RequestPlanetEphemerisWithPagination(int planetId, int pageNumber, int pageSize)
        {
            return await _context.EphemerisPlanets
                .Where(e => e.PlanetId == planetId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }


        public void AddEphemerisToExistingPlanet(List<EphemerisPlanet> ephemeris, int horizonId) 
        {
            var planet = _context.Planets.FirstOrDefault(p => p.HorizonId == horizonId);
            if (planet != null)
            {
                foreach (var data in ephemeris) 
                {
                    planet.Ephemeris.Add(data);
                }
                _context.SaveChanges();
            }
        }

        public void AddMoonsToExistingPlanet(List<Moon> moons, int horizonId) 
        {
            var planet = _context.Planets.FirstOrDefault(p => p.HorizonId == horizonId);
            if (planet != null)
            {
                if (planet.Moons == null)
                {
                    planet.Moons = new List<Moon>();
                }

                planet.Moons.AddRange(moons);
                _context.SaveChanges();
            }
        }
    }

}