
namespace OurSolarSystemAPI.Models
{
    public class UserEntity
    {
        public int Id { get; set; } 
        public string Password { get; set; }
        public required string Username { get; set; }
        public string PasswordSalt { get; set; }
    }
}
