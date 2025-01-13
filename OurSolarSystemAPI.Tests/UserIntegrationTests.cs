using Xunit;
using OurSolarSystemAPI.Repository.MySQL;
using OurSolarSystemAPI.Models;
using Microsoft.EntityFrameworkCore;


public class UserIntegrationTests : IDisposable
{
    private OurSolarSystemContext _context;

    public UserIntegrationTests()
    {
        var options = new DbContextOptionsBuilder<OurSolarSystemContext>()
            .UseMySql("Server=localhost;Port=3306;Database=OurSolarSystem;Uid=root;Pwd=##Heisenberg##;",
                      new MySqlServerVersion(new Version(8, 0, 0)))
            .Options;

        _context = new OurSolarSystemContext(options);
        _context.Database.EnsureCreated(); // Sikrer, at tabellerne er oprettet
    }

    [Fact]
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
        Assert.NotNull(createdUser);
        Assert.Equal("testuser", createdUser.Username);
        Assert.Equal("securepassword", createdUser.Password);
        Assert.Equal("randomsalt", createdUser.PasswordSalt);
    }

    [Fact]
    public async Task CreateUser_ShouldFailIfUsernameAlreadyExists()
    {
        await Assert.ThrowsAsync<DbUpdateException>(async () =>
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
        });
    }

    [Fact]
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
        Assert.NotNull(updatedUser);
        Assert.Equal("newpassword", updatedUser.Password);
        Assert.Equal("newsalt", updatedUser.PasswordSalt);
    }

    [Fact]
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
        Assert.NotNull(retrievedUser);
        Assert.Equal("requestuser", retrievedUser.Username);
        Assert.Equal("randomsalt", retrievedUser.PasswordSalt);
    }

    [Fact]
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
        Assert.Null(deletedUser);
    }

    public void Dispose()
    {
        // Ryd op efter hver test
        _context.Users.RemoveRange(_context.Users);
        _context.SaveChanges();
        _context.Dispose();
    }
}