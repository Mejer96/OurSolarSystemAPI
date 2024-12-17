using OurSolarSystemAPI.Repository.MySQL;
using OurSolarSystemAPI.Models;
namespace OurSolarSystemAPI.Service.MySQL
{
    public class PlanetServiceMySQL 
    {
        private readonly PlanetRepositoryMySQL _planetRepo;
        private double scalingFactor = 1.914139801102884e-22;



        public PlanetServiceMySQL(PlanetRepositoryMySQL planetRepo) 
        {
            _planetRepo = planetRepo;
        }

        public Planet? RequestPlanetLocationByNameAndDateTime(string name, DateTime dateTime) 
        {
            return _planetRepo.RequestPlanetLocationByNameAndDateTime(name, dateTime);
        }

        public Planet? RequestPlanetLocationsByHorizonId(int horizonId) 
        {
            Planet planet = _planetRepo.RequestPlanetLocationByHorizonId(horizonId);
            foreach (EphemerisPlanet ephemeris in planet.Ephemeris) 
                {
                    ephemeris.ScaledPositionX = ScalePlanetCoordinates(ephemeris.PositionX);
                    ephemeris.ScaledPositionY = ScalePlanetCoordinates(ephemeris.PositionY);
                    ephemeris.ScaledPositionZ = ScalePlanetCoordinates(ephemeris.PositionZ);
                }
            return planet;
        }

        public async Task<List<EphemerisPlanet>> RequestPlanetEphemerisWithPagination(int horizonId, int pageNumber, int pageSize) 
        {
            List<EphemerisPlanet> data = await _planetRepo.RequestPlanetEphemerisWithPagination(horizonId, pageNumber, pageSize);

            foreach (EphemerisPlanet ephemeris in data) 
                {
                    ephemeris.ScaledPositionX = ScalePlanetCoordinates(ephemeris.PositionX);
                    ephemeris.ScaledPositionY = ScalePlanetCoordinates(ephemeris.PositionY);
                    ephemeris.ScaledPositionZ = ScalePlanetCoordinates(ephemeris.PositionZ);
                }
            return data;
        }

        public async Task<List<Planet>> requestAllPlanetsWithEphemeris() 
        {
            List<Planet> planets = await _planetRepo.requestAllPlanetsWithEphemeris();
            double scalingFactor = 1e+20;

            foreach (Planet planet in planets) 
            {
                foreach (EphemerisPlanet ephemeris in planet.Ephemeris) 
                {
                    ephemeris.ScaledPositionX = ScalePlanetCoordinates(ephemeris.PositionX);
                    ephemeris.ScaledPositionY = ScalePlanetCoordinates(ephemeris.PositionY);
                    ephemeris.ScaledPositionZ = ScalePlanetCoordinates(ephemeris.PositionZ);
                }
            }

            return planets;
        }
    public double ScalePlanetCoordinates(double position)
    {
        // Apply the scaling factor to each coordinate
        double scaledPosition = position * scalingFactor;


        return scaledPosition;
    }
    }
}