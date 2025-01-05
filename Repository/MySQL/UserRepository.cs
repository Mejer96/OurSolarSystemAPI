using OurSolarSystemAPI.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OurSolarSystemAPI.Repository.MySQL;
using OurSolarSystemAPI.Utility;

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
            .FirstOrDefaultAsync() ?? throw new Exception();

        }

        public async Task<UserEntity> GetUserByWithSaltAndPassword(string username) 
        {
            return await _context.Users.Where(u => u.Username == username)
            .FirstOrDefaultAsync() ?? throw new Exception();
        }

        public async Task<UserDto> GetUserByUsername(string username) 
        {
            return await _context.Users.Where(u => u.Username == username)
            .Select(u => new UserDto
                {
                    Username = u.Username,
                    Id = u.Id
                })
            .FirstOrDefaultAsync() ?? throw new Exception();
        }

        public async Task<UserDto> GetUserByUsernameAndPassword(string username, string password) 
        {
            return await _context.Users.Where(u => u.Username == username && u.Password == password)
            .Select(u => new UserDto
                {
                    Username = u.Username,
                    Id = u.Id
                })
            .FirstOrDefaultAsync() ?? throw new Exception();

        }

        public async Task<UserDto> CreateUser(UserEntity user) 
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

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

            return result > 0;
        }

        public async Task<UserDto> UpdateUser(UserEntity user) 
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new UserDto
                {
                    Username = user.Username,
                    Id = user.Id
                }; 
        }
    }
}
