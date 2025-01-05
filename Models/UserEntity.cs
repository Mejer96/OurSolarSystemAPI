
namespace OurSolarSystemAPI.Models
{
    public class UserEntity
    {
        public string Password;
        public int Id { get; set; } 
        public required string Username { get; set; }
        public string PasswordSalt { get; set; }
    }
}
