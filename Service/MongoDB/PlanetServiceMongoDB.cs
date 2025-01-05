using OurSolarSystemAPI.Service.Interfaces;
using OurSolarSystemAPI.Repository.MongoDB;
using System;

namespace OurSolarSystemAPI.Service.MongoDB 
{
    public class PlanetServiceMongoDB
    {
        private readonly PlanetRepositoryMongoDB _planetRepoMongoDB;

        public PlanetServiceMongoDB(PlanetRepositoryMongoDB planetRepoMongoDB) 
        {
            _planetRepoMongoDB = planetRepoMongoDB;
        }

        public async Task<(DateTime date, double distance)> GetDistanceBetween(int firstHorizonId, int secondHorizonId, DateTime date) 
        {
           (EphemerisMongoDTO planetLocationOne, EphemerisMongoDTO planetLocationTwo) = await _planetRepoMongoDB.GetDistance(firstHorizonId, secondHorizonId, date);

            double deltaX = planetLocationTwo.PositionX - planetLocationOne.PositionX;
            double deltaY = planetLocationTwo.PositionY - planetLocationOne.PositionY;
            double deltaZ = planetLocationTwo.PositionZ - planetLocationOne.PositionZ;

            double distance = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2) + Math.Pow(deltaZ, 2));

            return (date, distance);
        }

        public async Task<PlanetMongoDTO> GetByHorizonId(int horizonId) 
        {
            return await _planetRepoMongoDB.GetByHorizonId(horizonId);
        }


        public async Task<EphemerisMongoDTO> GetLocationByHorizonIdAndDate(int horizonId, DateTime date) 
        {

            return await _planetRepoMongoDB.GetLocationByHorizonIdAndDate(horizonId, date);
        }

        public async Task<List<EphemerisMongoDTO>> GetLocationsByHorizonId(int horizonId) 
        {
            return await _planetRepoMongoDB.GetLocationsByHorizonId(horizonId);
        }

        public async Task<PlanetMongoDTO> GetByName(string name) 
        {
            return await _planetRepoMongoDB.GetByName(name);
        }
    }
}