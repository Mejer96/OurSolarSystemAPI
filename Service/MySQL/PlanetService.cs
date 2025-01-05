using OurSolarSystemAPI.Repository.MySQL;
using OurSolarSystemAPI.Models;
using System.Numerics;
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
            return await _planetRepo.GetLocationsByHorizonId(horizonId);
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