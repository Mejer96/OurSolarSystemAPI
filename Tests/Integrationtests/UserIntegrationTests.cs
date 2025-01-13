using Microsoft.EntityFrameworkCore;
using OurSolarSystemAPI.Models;
using OurSolarSystemAPI.Repository.MySQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarSystemTest
{
    [TestClass]
    public class UserIntegrationTests
    {
        private OurSolarSystemContext _context;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<OurSolarSystemContext>()
                .UseMySql("Server=localhost;Port=3307;Database=OurSolarSystem2.0;Uid=root;Pwd=Kea2024vinter!;",
                          new MySqlServerVersion(new Version(8, 0, 0)))
                .Options;

            _context = new OurSolarSystemContext(options);
            _context.Database.EnsureCreated(); // Sikrer, at tabellerne er oprettet
        }

        [TestMethod]
        public async Task CreateUser_ShouldInsertUserIntoDatabase()
        {
            // Arrange
            var user = new UserEntity
            {
                Username = "testuser",
                Password = "securepassword",
                PasswordSalt = "randomsalt"
            };

            // Act
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Assert
            var createdUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == "testuser");
            Assert.IsNotNull(createdUser);
            Assert.AreEqual("testuser", createdUser.Username);
            Assert.AreEqual("securepassword", createdUser.Password);
            Assert.AreEqual("randomsalt", createdUser.PasswordSalt);
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public async Task CreateUser_ShouldFailIfUsernameAlreadyExists()
        {
            // Arrange
            var user1 = new UserEntity
            {
                Username = "duplicateuser",
                Password = "password1",
                PasswordSalt = "salt1"
            };

            var user2 = new UserEntity
            {
                Username = "duplicateuser", // Samme username som user1
                Password = "password2",
                PasswordSalt = "salt2"
            };

            // Act
            _context.Users.Add(user1);
            await _context.SaveChangesAsync();

            _context.Users.Add(user2);
            await _context.SaveChangesAsync(); // Forventet at kaste en undtagelse
        }

        [TestMethod]
        public async Task UpdateUser_ShouldModifyUserDetails()
        {
            // Arrange
            var user = new UserEntity
            {
                Username = "updatableuser",
                Password = "oldpassword",
                PasswordSalt = "oldsalt"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == "updatableuser");
            existingUser.Password = "newpassword";
            existingUser.PasswordSalt = "newsalt";
            await _context.SaveChangesAsync();

            // Assert
            var updatedUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == "updatableuser");
            Assert.IsNotNull(updatedUser);
            Assert.AreEqual("newpassword", updatedUser.Password);
            Assert.AreEqual("newsalt", updatedUser.PasswordSalt);
        }

        [TestMethod]
        public async Task GetUser_ShouldReturnCorrectUserWithoutPassword()
        {
            // Arrange
            var user = new UserEntity
            {
                Username = "requestuser",
                Password = "hiddenpassword",
                PasswordSalt = "randomsalt"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var retrievedUser = await _context.Users
                .Where(u => u.Username == "requestuser")
                .Select(u => new { u.Username, u.PasswordSalt })
                .FirstOrDefaultAsync();

            // Assert
            Assert.IsNotNull(retrievedUser);
            Assert.AreEqual("requestuser", retrievedUser.Username);
            Assert.AreEqual("randomsalt", retrievedUser.PasswordSalt);
        }

        [TestMethod]
        public async Task DeleteUser_ShouldRemoveUserFromDatabase()
        {
            // Arrange
            var user = new UserEntity
            {
                Username = "deletableuser",
                Password = "password",
                PasswordSalt = "salt"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == "deletableuser");
            _context.Users.Remove(existingUser);
            await _context.SaveChangesAsync();

            // Assert
            var deletedUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == "deletableuser");
            Assert.IsNull(deletedUser);
        }

        [TestCleanup]
        public void Teardown()
        {
            // Ryd op efter hver test
            _context.Users.RemoveRange(_context.Users);
            _context.SaveChanges();
            _context.Dispose();
        }
    }
}
