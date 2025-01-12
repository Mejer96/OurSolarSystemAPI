namespace OurSolarSystemAPI.Service.Interfaces
{
    interface IPlanetService<T>
    {
        public T GetDistanceBetween<T>(int firstHorizonId, int secondHorizonId, DateTime date);

        public T GetLocationByHorizonIdAndDate<T>(int horizonId, DateTime date);

        public T GetByHorizonId<T>(int horizonId);

        public T GetLocationsByHorizonId<T>(int horizonId);

        public T GetByName<T>(string name);
    }
}