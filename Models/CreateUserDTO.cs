namespace OurSolarSystemAPI.Models
{
    public class CreateUserDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string RepeatedPassword { get; set; }
    }

}
