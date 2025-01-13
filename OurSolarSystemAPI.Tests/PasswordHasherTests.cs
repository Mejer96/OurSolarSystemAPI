using OurSolarSystemAPI.Auth;
using Xunit;

namespace OurSolarSystemAPI
{
    public class PasswordHasherTest
    {
        [Theory]
        [InlineData("password123", "salt123")]
        [InlineData("anotherPassword", "differentSalt")]
        [InlineData("yetAnotherPassword", "yetAnotherSalt")]
        public void HashPasswordWithSalt_ShouldReturnValidHash(string password, string salt)
        {
            // Act
            string hash = PasswordHasher.HashPasswordWithSalt(password, salt);

            // Assert
            Assert.NotNull(hash);
            Assert.NotEmpty(hash);
        }

        [Theory]
        [InlineData(16)]
        [InlineData(32)]
        [InlineData(64)]
        public void GenerateSalt_ShouldReturnValidSalt(int length)
        {
            // Act
            var (saltBytes, saltString) = PasswordHasher.GenerateSalt(length);

            // Assert
            Assert.NotNull(saltBytes);
            Assert.Equal(length, saltBytes.Length);
            Assert.NotNull(saltString);
            Assert.NotEmpty(saltString);
        }
    }
}
