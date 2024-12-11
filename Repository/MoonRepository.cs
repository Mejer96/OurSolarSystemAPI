using Microsoft.EntityFrameworkCore;
using OurSolarSystemAPI.Models;
using Dapper;

namespace OurSolarSystemAPI.Repository
{
    public class MoonRepository 
    {
        private readonly OurSolarSystemContext _context;

        public MoonRepository(OurSolarSystemContext context) 
        {
            _context = context;
        }

        public List<MoonLocation> GetMoonLocations(DateTime startDate, DateTime endDate, int planetId)
        {
            var locations = new List<MoonLocation>();

            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetMoonLocations"; // Stored procedure name
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

                    var planetIdParam = command.CreateParameter();
                    planetIdParam.ParameterName = "@PlanetId";
                    planetIdParam.Value = planetId;
                    command.Parameters.Add(planetIdParam);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            locations.Add(new MoonLocation
                            {
                                MoonName = reader["MoonName"].ToString(),
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

        public List<MoonsWithHorizon> GetMoonsWithHorizon()
        {
            var query = "SELECT * FROM MoonsWithHorizon";
            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();
                return connection.Query<MoonsWithHorizon>(query).ToList();
            }
        }


        // public Moon RequestCurrentMoonLocationByHorizonId(int horizonId) 
        // {
        //     return _context.Moons
        //         .Where(m => m.HorizonId == horizonId)
        //         .Include(m => m.Ephemeris.Where(e => e.DateTime.Date == dateTime.Date))
        //         .FirstOrDefault();
        // }
    }

}