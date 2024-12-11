using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using OurSolarSystemAPI.Models;

namespace OurSolarSystemAPI.Repository 
{
    public class BarycenterRepository
    {
        private readonly OurSolarSystemContext _context;

        public BarycenterRepository(OurSolarSystemContext context) 
        {
            _context = context;
        }

        public void CreateBarycenter(Barycenter barycenter) 
        {
            _context.Barycenters.Add(barycenter);
            _context.SaveChanges();
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


        public List<BarycenterLocation> GetBarycenterLocations(DateTime startDate, DateTime endDate, int horizonId)
        {
            var locations = new List<BarycenterLocation>();

            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetBarycenterLocations"; // Stored procedure name
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // Add parameters
                    var startDateParam = command.CreateParameter();
                    startDateParam.ParameterName = "@StartDate";
                    startDateParam.Value = startDate;
                    command.Parameters.Add(startDateParam);

                    var endDateParam = command.CreateParameter();
                    endDateParam.ParameterName = "@EndDate";
                    endDateParam.Value = endDate;
                    command.Parameters.Add(endDateParam);

                    var horizonIdParam = command.CreateParameter();
                    horizonIdParam.ParameterName = "@HorizonId";
                    horizonIdParam.Value = horizonId;
                    command.Parameters.Add(horizonIdParam);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            locations.Add(new BarycenterLocation
                            {
                                BarycenterName = reader["BarycenterName"].ToString(),
                                PositionX = Convert.ToDouble(reader["PositionX"]),
                                PositionY = Convert.ToDouble(reader["PositionY"]),
                                PositionZ = Convert.ToDouble(reader["PositionZ"])
                            });
                        }
                    }
                }
            }

            return locations;
        }

    }



}