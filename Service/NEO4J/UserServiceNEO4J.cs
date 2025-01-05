using OurSolarSystemAPI.Models;
using OurSolarSystemAPI.Repository.NEO4J;
using OurSolarSystemAPI.Auth;

namespace OurSolarSystemAPI.Service.NEO4J 
{
    public class UserServiceNEO4J 
    {
        private readonly UserRepositoryNEO4J _userRepo;

        public UserServiceNEO4J(UserRepositoryNEO4J userRepo) 
        {
            _userRepo = userRepo;
        }


        public async Task<UserDtoResponse> CreateUser(string username, string password, string repeatedPassword) 
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

        public async Task<bool> DeleteUser(string username, string password, string repeatedPassword) 
        {
            if (password != repeatedPassword) throw new Exception("Passwords doesn't match");

            UserDtoResponseWithPasswordAndSalt user = await _userRepo.GetUserWithPasswordAndSalt(username);

            string hashedPassword = PasswordHasher.HashPasswordWithSalt(password, user.Salt);

            if (hashedPassword != user.Password) throw new Exception("Incorrect password");

            return await _userRepo.DeleteUserById(user.Id);

        }

        public async Task<UserDtoResponse> GetUserByUsername(string username) 
        {
            return await _userRepo.GetUserByUsername(username);
        }

        public async Task<bool> UpdateUsername(string oldUsername, string newUsername, string password, string repeatedPassword) 
        {
            if (password != repeatedPassword) throw new Exception("Passwords doesn't match");

            UserDtoResponseWithPasswordAndSalt user = await _userRepo.GetUserWithPasswordAndSalt(oldUsername);
            string hashedPassword = PasswordHasher.HashPasswordWithSalt(password, user.Salt);

            if (hashedPassword != user.Password) throw new Exception("Incorrect password");

            return await _userRepo.UpdateUsername(user.Id, newUsername);
        }

        public async Task<bool> UpdatePassword(string username, string oldPassword, string repeatedOldPassword, string newPassword, string repeatedNewPassword) 
        {
            if (newPassword != repeatedNewPassword) throw new Exception("Passwords doesn't match");
            if (oldPassword != repeatedOldPassword) throw new Exception("Passwords doesn't match");

            UserDtoResponseWithPasswordAndSalt user = await _userRepo.GetUserWithPasswordAndSalt(username);
            string hashedPassword = PasswordHasher.HashPasswordWithSalt(oldPassword, user.Salt);

            if (hashedPassword != user.Password) throw new Exception("Incorrect password");

            (byte[] saltBytes, string salt) = PasswordHasher.GenerateSalt();
            string hashedNewPassword = PasswordHasher.HashPasswordWithSalt(newPassword, salt);

            return await _userRepo.UpdatePassword(hashedNewPassword, salt);
        }
        
    }
}