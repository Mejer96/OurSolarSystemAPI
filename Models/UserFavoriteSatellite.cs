
namespace OurSolarSystemAPI.Models
{
    public class UserFavoriteSatellite
    {
        public int Id { get; set; }
        public required int UserId { get; set; }
        public required UserEntity User { get; set; }

    }
}