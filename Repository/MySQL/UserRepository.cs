using OurSolarSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
using OurSolarSystemAPI.Repository.MySQL;
using OurSolarSystemAPI.Exceptions;

namespace OurSolarSystemAPI.Repository
{
    public class UserDto 
    {
        public required string Username { get; set; }
        public required int Id { get; set; }
    }
    public class UserRepositoryMySQL
    {

        private readonly OurSolarSystemContext _context;

        public UserRepositoryMySQL(OurSolarSystemContext context) 
        {
            _context = context;
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            return await _context.Users
                .Select(u => new UserDto
                {
                    Username = u.Username,
                    Id = u.Id
                })
                .ToListAsync();
        }

        public async Task<UserDto> GetUserById(int id) 
        {
            return await _context.Users.Where(u => u.Id == id)
            .Select(u => new UserDto
                {
                    Username = u.Username,
                    Id = u.Id
                })
            .FirstOrDefaultAsync() ?? throw new EntityNotFound("No user found by that id");

        }

        public async Task<UserEntity> GetUserByNameWithSaltAndPassword(string username) 
        {
            return await _context.Users.Where(u => u.Username == username)
            .FirstOrDefaultAsync() ?? throw new EntityNotFound("No user found by that id");
        }

        public async Task<UserDto> GetUserByUsername(string username) 
        {
            return await _context.Users.Where(u => u.Username == username)
            .Select(u => new UserDto
                {
                    Username = u.Username,
                    Id = u.Id
                })
            .FirstOrDefaultAsync() ?? throw new EntityNotFound("No user found by that username");
        }

        public async Task<UserDto> GetUserByUsernameAndPassword(string username, string password) 
        {
            return await _context.Users.Where(u => u.Username == username && u.Password == password)
            .Select(u => new UserDto
                {
                    Username = u.Username,
                    Id = u.Id
                })
            .FirstOrDefaultAsync() ?? throw new EntityNotFound("No user found by that id");

        }

        public async Task<UserDto> CreateUser(UserEntity user)
        {

            await _context.Users.AddAsync(user);
            var result = await _context.SaveChangesAsync();

            if (result != 1) throw new SomethingWentWrong("Something went wrong. User not created");

            return new UserDto
                {
                    Username = user.Username,
                    Id = user.Id
                };
        }

        public async Task<bool> DeleteUser(int userId) 
        {
            var result = await _context.Users
            .Where(u => u.Id == userId)
            .ExecuteDeleteAsync();

            if (result != 1) throw new EntityNotFound("No user found by that id");
  
            return true;
        }

        public async Task<UserDto> UpdateUser(UserEntity user) 
        {
            _context.Users.Update(user);
            var result = await _context.SaveChangesAsync();

             if (result != 1) throw new EntityNotFound("User not updated");

            return new UserDto
                {
                    Username = user.Username,
                    Id = user.Id
                }; 
        }
    }
}
