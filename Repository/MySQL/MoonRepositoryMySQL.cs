using Microsoft.EntityFrameworkCore;
using OurSolarSystemAPI.Models;

namespace OurSolarSystemAPI.Repository.MySQL
{
    public class MoonRepositoryMySQL
    {
        private readonly OurSolarSystemContext _context;

        public MoonRepositoryMySQL(OurSolarSystemContext context)
        {
            _context = context;
        }

        public async Task<List<Moon>> requestAllMoonsWithEphemeris()
        {
            return await _context.Moons
            .Include(m => m.Ephemeris)
            .ToListAsync();
        }

        public async Task addMoonsToExistingPlanetAndBarycenter(List<Moon> moons, int planetHorizonId, int barycenterHorizonId)
        {
            Barycenter barycenter = await _context.Barycenters.FirstOrDefaultAsync(b => b.HorizonId == barycenterHorizonId);
            Planet planet = await _context.Planets.FirstOrDefaultAsync(p => p.HorizonId == planetHorizonId);

            if (barycenter != null && planet != null)
            {
                _context.Attach(planet);

                barycenter.Moons ??= new List<Moon>();
                barycenter.Moons.AddRange(moons);

                planet.Moons ??= new List<Moon>();
                planet.Moons.AddRange(moons);

                await _context.SaveChangesAsync();
            }
        }
    }

}