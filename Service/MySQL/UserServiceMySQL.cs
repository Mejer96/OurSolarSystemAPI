using OurSolarSystemAPI.Auth;
using OurSolarSystemAPI.Models;
using OurSolarSystemAPI.Repository;
namespace OurSolarSystemAPI.Service.MySQL
{
    public class UserServiceMySQL
    {
        private readonly UserRepositoryMySQL _userRepo;

        public UserServiceMySQL(UserRepositoryMySQL userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<UserDto> CreateUser(string username, string password, string repeatedPassword)
        {

            if (password != repeatedPassword) throw new Exception("Passwords doesn't match");

            (byte[] saltBytes, string salt) = PasswordHasher.GenerateSalt();
            string hashedPassword = PasswordHasher.HashPasswordWithSalt(password, salt);

            var user = new UserEntity
            {
                Username = username,
                Password = hashedPassword,
                PasswordSalt = salt,
            };

            return await _userRepo.CreateUser(user);

        }

        public async Task<UserDto> GetUserByUsername(string username)
        {
            return await _userRepo.GetUserByUsername(username);
        }

        public async Task<bool> DeleteUser(string username, string password, string repeatedPassword)
        {
            if (password != repeatedPassword) throw new Exception("Passwords doesn't match");

            UserEntity user = await _userRepo.GetUserByWithSaltAndPassword(username);

            string hashedPassword = PasswordHasher.HashPasswordWithSalt(password, user.PasswordSalt);

            if (hashedPassword != user.Password) throw new Exception("Incorrect password");

            return await _userRepo.DeleteUser(user.Id);

        }

        public async Task<UserDto> UpdatePassword(string username, string oldPassword, string repeatedOldPassword, string newPassword, string repeatedNewPassword)
        {
            if (newPassword != repeatedNewPassword) throw new Exception("Passwords doesn't match");
            if (oldPassword != repeatedOldPassword) throw new Exception("Passwords doesn't match");

            UserEntity user = await _userRepo.GetUserByWithSaltAndPassword(username);
            string hashedPassword = PasswordHasher.HashPasswordWithSalt(oldPassword, user.PasswordSalt);

            if (hashedPassword != user.Password) throw new Exception("Incorrect password");

            (byte[] saltBytes, string salt) = PasswordHasher.GenerateSalt();
            string hashedNewPassword = PasswordHasher.HashPasswordWithSalt(newPassword, salt);

            user.Password = hashedNewPassword;
            user.PasswordSalt = salt;

            return await _userRepo.UpdateUser(user);
        }

        public async Task<UserDto> UpdateUsername(string oldUsername, string newUsername, string password, string repeatedPassword)
        {
            if (password != repeatedPassword) throw new Exception("Passwords doesn't match");

            UserEntity user = await _userRepo.GetUserByWithSaltAndPassword(oldUsername);
            string hashedPassword = PasswordHasher.HashPasswordWithSalt(password, user.PasswordSalt);

            if (hashedPassword != user.Password) throw new Exception("Incorrect password");

            user.Username = newUsername;
            return await _userRepo.UpdateUser(user);
        }
    }
}