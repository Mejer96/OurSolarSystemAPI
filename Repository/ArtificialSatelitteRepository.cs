using Microsoft.EntityFrameworkCore;
using OurSolarSystemAPI.Models;
using OurSolarSystemAPI.Repository;

namespace OurSolarSystemAPI.Repository {

    public class ArtificialSatelliteRepository 
    {
        private readonly OurSolarSystemContext _context;

        public ArtificialSatelliteRepository(OurSolarSystemContext context) 
        {
            _context = context;
        }

        public void AddSatellite(ArtificialSatellite satellite) 
        {
            _context.ArtificialSatellites.Add(satellite);
            _context.SaveChanges();
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

        public void LogSatelliteSearch(int noradId)
        {
            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "LogSatelliteSearch"; // Stored procedure name
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var noradIdParam = command.CreateParameter();
                    noradIdParam.ParameterName = "@NoradId";
                    noradIdParam.Value = noradId;
                    command.Parameters.Add(noradIdParam);

                    command.ExecuteNonQuery();
                }
            }
        }

        public int GetSumOfSatelliteOrbits()
        {
            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    // Call the SumSatelliteOrbits function
                    command.CommandText = "SELECT SumSatelliteOrbits() AS TotalOrbits";
                    command.CommandType = System.Data.CommandType.Text;

                    var result = command.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
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