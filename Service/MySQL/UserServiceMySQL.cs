using OurSolarSystemAPI.Auth;
using OurSolarSystemAPI.Models;
using OurSolarSystemAPI.Repository;
using OurSolarSystemAPI.Exceptions;

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

            if (password != repeatedPassword) throw new PasswordsNotMatching("Passwords are not matching");

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

        public async Task<Boolean> GetUserByUsernameAndPassword(string username, string password) 
        {
            UserEntity user = await _userRepo.GetUserByNameWithSaltAndPassword(username);
            string hashedPassword = PasswordHasher.HashPasswordWithSalt(password, user.PasswordSalt);

            if (hashedPassword != user.Password) throw new AuthenticationFailed("authentication failed");

            return true;

        }

        public async Task<bool> DeleteUser(string username, string password, string repeatedPassword) 
        {
            if (password != repeatedPassword) throw new PasswordsNotMatching("Passwords are not matching");

            UserEntity user = await _userRepo.GetUserByNameWithSaltAndPassword(username);

            string hashedPassword = PasswordHasher.HashPasswordWithSalt(password, user.PasswordSalt);

            if (hashedPassword != user.Password) throw new InvalidPassword("Incorrect password");

            return await _userRepo.DeleteUser(user.Id);

        }

        public async Task<UserDto> UpdatePassword(string username, string oldPassword, string repeatedOldPassword, string newPassword, string repeatedNewPassword) 
        {
            if (newPassword != repeatedNewPassword) throw new PasswordsNotMatching("New Passwords are not matching");
            if (oldPassword != repeatedOldPassword) throw new PasswordsNotMatching("Old Passwords are not matching");

            UserEntity user = await _userRepo.GetUserByNameWithSaltAndPassword(username);
            string hashedPassword = PasswordHasher.HashPasswordWithSalt(oldPassword, user.PasswordSalt);

            if (hashedPassword != user.Password) throw new InvalidPassword("Incorrect password");

            (byte[] saltBytes, string salt) = PasswordHasher.GenerateSalt();
            string hashedNewPassword = PasswordHasher.HashPasswordWithSalt(newPassword, salt);

            user.Password = hashedNewPassword;
            user.PasswordSalt = salt;

            return await _userRepo.UpdateUser(user);
        }

        public async Task<UserDto> UpdateUsername(string oldUsername, string newUsername, string password, string repeatedPassword) 
        {
            if (password != repeatedPassword) throw new PasswordsNotMatching("Passwords are not matching");
            
            UserEntity user = await _userRepo.GetUserByNameWithSaltAndPassword(oldUsername);
            string hashedPassword = PasswordHasher.HashPasswordWithSalt(password, user.PasswordSalt);

            if (hashedPassword != user.Password) throw new InvalidPassword("Incorrect password");

            user.Username = newUsername;
            return await _userRepo.UpdateUser(user);
        }  
    }
}