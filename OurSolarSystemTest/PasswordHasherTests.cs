using OurSolarSystemAPI.Auth;
using Xunit;

namespace OurSolarSystemTest
{
    public class PasswordHasherTests
    {
        [Fact]
        public void HashPasswordWithSalt_ShouldReturnHashedPassword()
        {
            // Arrange
            string password = "TestPassword";
            string salt = "U29tZVNhbHQ="; // Base64 for "SomeSalt"

            // Act
            string hashedPassword = PasswordHasher.HashPasswordWithSalt(password, salt);

            // Assert
            Assert.NotNull(hashedPassword);
            Assert.NotEmpty(hashedPassword);
        }

        [Fact]
        public void GenerateSalt_ShouldReturnSaltOfSpecifiedLength()
        {
            // Arrange
            int saltLength = 16;

            // Act
            var (saltBytes, saltString) = PasswordHasher.GenerateSalt(saltLength);

            // Assert
            Assert.NotNull(saltBytes);
            Assert.Equal(saltLength, saltBytes.Length);
            Assert.NotNull(saltString);
            Assert.NotEmpty(saltString);
        }

        [Fact]
        public void GenerateSalt_ShouldReturnDifferentSalts()
        {
            // Act
            var (saltBytes1, saltString1) = PasswordHasher.GenerateSalt();
            var (saltBytes2, saltString2) = PasswordHasher.GenerateSalt();

            // Assert
            Assert.NotEqual(saltString1, saltString2);
        }
    }
}

