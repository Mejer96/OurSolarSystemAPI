using OurSolarSystemAPI.Repository.NEO4J;

namespace OurSolarSystemAPI.Service.NEO4J
{
    public class PlanetServiceNEO4J
    {
        private readonly PlanetRepositoryNEO4J _planetRepo;

        public PlanetServiceNEO4J(PlanetRepositoryNEO4J planetRepo)
        {
            _planetRepo = planetRepo;
        }

        public async Task<string> GetDistance(int firstHorizonId, int secondHorizonId, DateTime date)
        {
            return await _planetRepo.GetDistanceBetween(firstHorizonId, secondHorizonId, date);
        }

        public async Task<string> GetByHorizonId(int horizonId)
        {
            return await _planetRepo.GetByHorizonId(horizonId);
        }

        public async Task<string> GetByName(string name)
        {
            return await _planetRepo.GetByName(name);
        }

        public async Task<string> GetAttribute(int horizonId, string attribute)
        {
            return await _planetRepo.GetAttribute(horizonId, attribute);

        }

        public async Task<string> GetLocationByHorizonIdAndDate(int horizonID, DateTime date)
        {
            return await _planetRepo.GetLocationByHorizonIdAndDate(horizonID, date);

        }

        public async Task<string> GetLocationsByHorizonId(int horizonID)
        {
            return await _planetRepo.GetLocationsByHorizonId(horizonID);
        }
    }
}