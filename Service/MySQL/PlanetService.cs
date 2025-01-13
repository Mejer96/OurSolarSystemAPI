using OurSolarSystemAPI.Models;
using OurSolarSystemAPI.Repository.MySQL;
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

        public async Task<List<Planet>> GetAll() 
        {
            return await _planetRepo.GetAll();

        }

        public async Task<DistanceResult> GetDistance(int firstHorizonId, int secondHorizonId, DateTime date) 
        {
            return await _planetRepo.GetDistance(firstHorizonId, secondHorizonId, date);

        }

        public async Task<Planet> GetByHorizonId(int horizonId)
        {
            return await _planetRepo.GetByHorizonId(horizonId);

        }


        public async Task<Planet> GetByName(string name)
        {
            return await _planetRepo.GetByName(name);
        }

        public async Task<Planet> GetLocationByHorizonIdAndDate(int horizonId, DateTime date)
        {
            return await _planetRepo.GetLocationByHorizonIdAndDate(horizonId, date);

        }

        public async Task<Planet> GetLocationsByHorizonId(int horizonId)
        {
            var planet = await _planetRepo.GetLocationsByHorizonId(horizonId);
            var secondsInAYear = 365 * 24 * 60 * 60;

            double scalingFactor = 1.738528300513934e-22;
            foreach (EphemerisPlanet ephemeris in planet.Ephemeris) 
            {
                    ephemeris.ScaledPositionX = ephemeris.PositionX * scalingFactor;
                    ephemeris.ScaledPositionY = ephemeris.PositionY * scalingFactor;
                    ephemeris.ScaledPositionZ = ephemeris.PositionZ * scalingFactor;
                    double scaledVelocityPerSecondsX = ephemeris.VelocityX * scalingFactor;
                    double scaledVelocityPerSecondsY = ephemeris.VelocityY * scalingFactor;
                    double scaledVelocityPerSecondsZ = ephemeris.VelocityZ * scalingFactor;
                    ephemeris.ScaledVelocityX = scaledVelocityPerSecondsX * secondsInAYear;
                    ephemeris.ScaledVelocityY = scaledVelocityPerSecondsY * secondsInAYear;
                    ephemeris.ScaledVelocityZ = scaledVelocityPerSecondsZ * secondsInAYear;
            }

            return planet;
        }


        public async Task<List<EphemerisPlanet>> GetEphemerisWithPagination(int horizonId, int pageNumber, int pageSize)
        {
            List<EphemerisPlanet> data = await _planetRepo.RequestPlanetEphemerisWithPagination(horizonId, pageNumber, pageSize);
            return data;
        }

        public async Task<List<Planet>> GetAllPlanetsWithEphemeris()
        {
            List<Planet> planets = await _planetRepo.GetAllPlanetsWithEphemeris();

            return planets;
        }

    }
}