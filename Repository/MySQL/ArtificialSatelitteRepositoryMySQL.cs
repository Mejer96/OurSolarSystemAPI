using Microsoft.EntityFrameworkCore;
using OurSolarSystemAPI.Models;


namespace OurSolarSystemAPI.Repository.MySQL  {

    public class ArtificialSatelliteRepositoryMySQL 
    {
        private readonly OurSolarSystemContext _context;

        public ArtificialSatelliteRepositoryMySQL(OurSolarSystemContext context) 
        {
            _context = context;
        }

        public void AddSatellite(ArtificialSatellite satellite) 
        {
            _context.ArtificialSatellites.Add(satellite);
            _context.SaveChanges();
        }

        public async Task<List<ArtificialSatellite>> RequestAllSatellites() 
        {
            return await _context.ArtificialSatellites
            .Include(s => s.Tle)
            .ToListAsync();
        } 

        public ArtificialSatellite? RequestSatelitteByNoradId(int noradId) 
        {
            return _context.ArtificialSatellites.FirstOrDefault(s => s.NoradId == noradId);
        }

        public ArtificialSatellite? RequestSatelliteLocationByNoradIdAndDateTime(int noradId, DateTime dateTime)
        {
            return _context.ArtificialSatellites
                .Where(s => s.NoradId == noradId)
                .Include(s => s.Tle.Where(t => t.IsArchived == false))
                .FirstOrDefault();
        }

        public async Task<bool> CheckIfSatelliteExistsByNoradId(int noradId) 
        {
            return await _context.ArtificialSatellites.AnyAsync(s => s.NoradId == noradId);
        }

        public async Task CreateTleToExistingSatellite(int noradId, TleArtificialSatellite tle) 
        {
            var satellite = await _context.ArtificialSatellites
                                        .Include(s => s.Tle) 
                                        .FirstOrDefaultAsync(s => s.NoradId == noradId);
            satellite.Tle.Add(tle);

            await _context.SaveChangesAsync();

        }

        // public void AddEphemerisToExistingSatellite(OurSolarSystemContext context, List<EphemerisArtificialSatellite> ephemeris, int satelliteId) 
        // {
        //     var satellite = context.ArtificialSatellites.FirstOrDefault(s => s.Id == satelliteId);
        //     if (satellite != null)
        //     {
        //         foreach (var data in ephemeris) 
        //         {
        //             satellite.Ephemeris.Add(data);
        //         }
        //         context.SaveChanges();
        //     }
        // }
    }

}