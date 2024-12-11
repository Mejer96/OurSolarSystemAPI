using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using OurSolarSystemAPI.Models;
using Dapper;

namespace OurSolarSystemAPI.Repository 
{

    public class PlanetRepository 
    {

        private readonly OurSolarSystemContext _context;

        public PlanetRepository(OurSolarSystemContext context) 
        {
            _context = context;
        }

        public void CreatePlanet(Planet planet) 
        {
            _context.Planets.Add(planet);
            _context.SaveChanges();
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

        public Planet? RequestPlanetByName(string planetName) 
        {
            return _context.Planets.FirstOrDefault(p => p.Name == planetName);
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

        public List<PlanetLocation> GetPlanetLocations(DateTime startDate, DateTime endDate)
        {
            // List to store results
            var locations = new List<PlanetLocation>();

            // Open a raw SQL connection
            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetPlanetLocations"; // Stored procedure name
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // Add parameters for the stored procedure
                    var startDateParam = command.CreateParameter();
                    startDateParam.ParameterName = "@StartDate";
                    startDateParam.Value = startDate;
                    command.Parameters.Add(startDateParam);

                    var endDateParam = command.CreateParameter();
                    endDateParam.ParameterName = "@EndDate";
                    endDateParam.Value = endDate;
                    command.Parameters.Add(endDateParam);

                    // Execute the command
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            locations.Add(new PlanetLocation
                            {
                                PlanetName = reader["PlanetName"].ToString(),
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

        public decimal GetTotalPlanetMass()
        {
            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    // Call the TotalPlanetMass function
                    command.CommandText = "SELECT TotalPlanetMass() AS TotalMass";
                    command.CommandType = System.Data.CommandType.Text;

                    var result = command.ExecuteScalar();
                    return result != null ? Convert.ToDecimal(result) : 0;
                }
            }
        }

        public List<PlanetOverview> GetPlanetOverview()
        {
            var query = "SELECT * FROM PlanetOverview";
            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();
                return connection.Query<PlanetOverview>(query).ToList();
            }
        }


    }


}

