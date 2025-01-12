using OurSolarSystemAPI.Auth;
using OurSolarSystemAPI.Repository.MongoDB;

namespace OurSolarSystemAPI.Service.MongoDB
{
    public class UserServiceMongoDB
    {
        private readonly UserRepositoryMongoDB _userRepo;

        public UserServiceMongoDB(UserRepositoryMongoDB userRepo)
        {
            _userRepo = userRepo;
        }


        public async Task<UserDtoResponse> CreateUser(string username, string password, string repeatedPassword)
        {

            if (password != repeatedPassword) throw new Exception("Passwords doesn't match");

            (byte[] saltBytes, string salt) = PasswordHasher.GenerateSalt();
            string hashedPassword = PasswordHasher.HashPasswordWithSalt(password, salt);
            string hashedRepeatPassword = PasswordHasher.HashPasswordWithSalt(repeatedPassword, salt);

            var user = new UserDTOMongoDB
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

            UserDTOMongoDB user = await _userRepo.GetUserWithPasswordAndSalt(username);

            string hashedPassword = PasswordHasher.HashPasswordWithSalt(password, user.PasswordSalt);

            if (hashedPassword != user.Password) throw new Exception("Incorrect password");

            return await _userRepo.DeleteUser(user.Id);

        }

        public async Task<Boolean> UpdatePassword(string username, string oldPassword, string repeatedOldPassword, string newPassword, string repeatedNewPassword)
        {
            if (newPassword != repeatedNewPassword) throw new Exception("Passwords doesn't match");
            if (oldPassword != repeatedOldPassword) throw new Exception("Passwords doesn't match");

            UserDTOMongoDB user = await _userRepo.GetUserWithPasswordAndSalt(username);
            string hashedPassword = PasswordHasher.HashPasswordWithSalt(oldPassword, user.PasswordSalt);

            if (hashedPassword != user.Password) throw new Exception("Incorrect password");

            (byte[] saltBytes, string salt) = PasswordHasher.GenerateSalt();
            string hashedNewPassword = PasswordHasher.HashPasswordWithSalt(newPassword, salt);

            return await _userRepo.UpdatePassword(user.Id, hashedNewPassword, salt);
        }

        public async Task<Boolean> UpdateUsername(string oldUsername, string newUsername, string password, string repeatedPassword)
        {
            if (password != repeatedPassword) throw new Exception("Passwords doesn't match");

            UserDTOMongoDB user = await _userRepo.GetUserWithPasswordAndSalt(oldUsername);
            string hashedPassword = PasswordHasher.HashPasswordWithSalt(password, user.PasswordSalt);

            if (hashedPassword != user.Password) throw new Exception("Incorrect password");

            return await _userRepo.UpdateUsername(user.Id, newUsername);

        }

    }
}