using OurSolarSystemAPI.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Xunit;

namespace OurSolarSystemTest
{
    public class JwtTokenGeneratorTest
    {
        [Fact]
        public void GenerateJwtToken_ShouldReturnValidToken()
        {
            // Arrange
            string username = "testuser";
            string role = "admin";

            // Act
            string token = JwtTokenGenerator.GenerateJwtToken(username, role);

            // Assert
            Assert.NotNull(token);
            Assert.NotEmpty(token);
        }

        [Fact]
        public void GenerateJwtToken_ShouldContainUsernameClaim()
        {
            // Arrange
            string username = "testuser";
            string role = "admin";

            // Act
            string token = JwtTokenGenerator.GenerateJwtToken(username, role);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // Assert
            Assert.Contains(jwtToken.Claims, c => c.Type == ClaimTypes.Name && c.Value == username);
        }

        [Fact]
        public void GenerateJwtToken_ShouldContainRoleClaim()
        {
            // Arrange
            string username = "testuser";
            string role = "admin";

            // Act
            string token = JwtTokenGenerator.GenerateJwtToken(username, role);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // Assert
            Assert.Contains(jwtToken.Claims, c => c.Type == ClaimTypes.Role && c.Value == role);
        }

        [Fact]
        public void GenerateJwtToken_ShouldHaveValidExpiration()
        {
            // Arrange
            string username = "testuser";
            string role = "admin";

            // Act
            string token = JwtTokenGenerator.GenerateJwtToken(username, role);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // Assert
            Assert.True(jwtToken.ValidTo > DateTime.UtcNow);
        }
    }
}
