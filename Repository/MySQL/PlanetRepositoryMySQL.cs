using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using OurSolarSystemAPI.Models;



namespace OurSolarSystemAPI.Repository.MySQL
{
    public class DistanceResult
    {
        public DateTime DateTime { get; set; }
        public double Distance { get; set; }
    }

    public class PlanetRepositoryMySQL
    {

        private readonly OurSolarSystemContext _context;

        public PlanetRepositoryMySQL(OurSolarSystemContext context)
        {
            _context = context;
        }

        public void AddPlanet(Planet planet)
        {
            _context.Planets.Add(planet);
            _context.SaveChanges();
        }

        public void AddSun(Star sun)
        {
            _context.Sun.Add(sun);
            _context.SaveChanges();
        }

        public async Task<Star> GetSunWithEphemeris()
        {
            return await _context.Sun
            .Include(s => s.Ephemeris)
            .FirstOrDefaultAsync(s => s.HorizonId == 10);
        }


        public async Task<List<Planet>> GetAllPlanetsWithMoons()
        {
            return await _context.Planets
            .Include(p => p.Moons)
            .ToListAsync();
        }

        public async Task<List<Planet>> GetAllPlanetsWithEphemeris()
        {
            return await _context.Planets
            .Include(p => p.Ephemeris)
            .ToListAsync();
        }

        public async Task<List<Planet>> GetAllPlanetsWithEphemerisAndMoons()
        {
            return await _context.Planets
            .Include(p => p.Ephemeris)
            .Include(p => p.Moons)
            .ToListAsync();
        }


        public async Task<Planet?> GetByHorizonId(int horizonId)
        {
            return await _context.Planets.FirstOrDefaultAsync(p => p.HorizonId == horizonId);
        }

        public async Task<Planet?> GetLocationsByHorizonId(int horizonId)
        {
            return await _context.Planets
                .Where(p => p.HorizonId == horizonId)
                .Include(p => p.Ephemeris)
                .FirstOrDefaultAsync();
        }

        public async Task<Planet?> GetByName(string name)
        {
            return await _context.Planets
                .Where(p => p.Name == name)
                .FirstOrDefaultAsync();
        }

        public async Task<Planet?> GetLocationByHorizonIdAndDate(int horizonId, DateTime date)
        {
            return await _context.Planets
                .Include(p => p.Ephemeris.Where(e => e.DateTime == date))
                .Where(p => p.HorizonId == horizonId)
                .FirstOrDefaultAsync();
        }

        public async Task<DistanceResult> GetDistance(int firstHorizonId, int secondHorizonId, DateTime date)
        {
            var result = await _context
                .Set<DistanceResult>()
                .FromSqlRaw(@"
                    SELECT 
                        e1.DateTime,
                        SQRT(
                            POW(e1.PositionX - e2.PositionX, 2) +
                            POW(e1.PositionY - e2.PositionY, 2) +
                            POW(e1.PositionZ - e2.PositionZ, 2)
                        ) AS distance
                    FROM 
                        ephemerisplanets e1
                    JOIN 
                        ephemerisplanets e2 ON e1.PlanetHorizonId = @firstHorizonId AND e2.PlanetHorizonId = @secondHorizonId
                    WHERE
                        e1.DateTime = @date
                        AND e2.DateTime = @date
                    LIMIT 1",
                    new MySqlParameter("@firstHorizonId", firstHorizonId),
                    new MySqlParameter("@secondHorizonId", secondHorizonId),
                    new MySqlParameter("@date", date.ToString("yyyy-MM-dd HH:mm:ss")))
                .FirstOrDefaultAsync();

            return result;
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