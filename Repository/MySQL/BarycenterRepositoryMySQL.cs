using Microsoft.EntityFrameworkCore;
using OurSolarSystemAPI.Models;

namespace OurSolarSystemAPI.Repository.MySQL
{
    public class BarycenterRepositoryMySQL
    {
        private readonly OurSolarSystemContext _context;

        public BarycenterRepositoryMySQL(OurSolarSystemContext context)
        {
            _context = context;
        }

        public async Task CreateBarycenter(Barycenter barycenter)
        {
            await _context.Barycenters.AddAsync(barycenter);
            await _context.SaveChangesAsync();
        }

        public async Task CreateSolarSystemBarycenter(SolarSystemBarycenter barycenter)
        {
            await _context.SolarSystemBaryCenter.AddAsync(barycenter);
            await _context.SaveChangesAsync();
        }

        public async Task AddSunToExistingSolarSystemBarycenter(Star sun)
        {
            SolarSystemBarycenter barycenter = await _context.SolarSystemBaryCenter.FirstOrDefaultAsync(b => b.HorizonId == 0);

            if (barycenter != null)
            {
                sun.SolarSystemBarycenter = barycenter;
                await _context.Sun.AddAsync(sun);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddBarycenterToExistingSolarSystemBarycenter(Barycenter barycenter)
        {
            SolarSystemBarycenter solarSystemBarycenter = await _context.SolarSystemBaryCenter.FirstOrDefaultAsync(s => s.HorizonId == 0);

            if (solarSystemBarycenter != null)
            {
                solarSystemBarycenter.Barycenters ??= new List<Barycenter>();
                solarSystemBarycenter.Barycenters.Add(barycenter);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddPlanetToExistingBarycenter(Planet planet, int horizonId)
        {
            Barycenter barycenter = await _context.Barycenters.FirstOrDefaultAsync(b => b.HorizonId == horizonId);

            if (barycenter != null)
            {
                barycenter.Planets ??= new List<Planet>();
                barycenter.Planets.Add(planet);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddMoonToExistingBarycenter(Moon moon, int horizonId)
        {
            Barycenter barycenter = await _context.Barycenters.FirstOrDefaultAsync(b => b.HorizonId == horizonId);

            if (barycenter != null)
            {
                barycenter.Moons ??= new List<Moon>();
                barycenter.Moons.Add(moon);
                _context.SaveChanges();
            }
        }


        public async Task<List<Barycenter>> requestAllBarycentersWithEphemeris()
        {
            return await _context.Barycenters
            .Include(b => b.Ephemeris)
            .ToListAsync();
        }

        public async Task<List<Barycenter>> requestAllBarycentersWithRelations()
        {
            return await _context.Barycenters
                .Include(b => b.Planets)
                    .ThenInclude(p => p.Moons)
                .Include(b => b.Ephemeris)
                .ToListAsync();
        }

        public Barycenter? RequestBarycenterLocationByNameAndDateTime(string name, DateTime dateTime)
        {
            return _context.Barycenters
                .Where(b => b.Name == name)
                .Include(b => b.Ephemeris.Where(e => e.DateTime.Date == dateTime.Date))
                .FirstOrDefault();
        }

        public Barycenter? RequestBarycenterLocationByHorizonIdAndDateTime(int horizonId, DateTime dateTime)
        {
            return _context.Barycenters
                .Where(b => b.HorizonId == horizonId)
                .Include(b => b.Ephemeris.Where(e => e.DateTime.Date == dateTime.Date))
                .FirstOrDefault();
        }

    }



}