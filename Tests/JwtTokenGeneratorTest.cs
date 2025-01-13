using OurSolarSystemAPI.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Xunit;

namespace OurSolarSystemAPI
{
    public class JwtTokenGeneratorTest
    {
        [Theory]
        [InlineData("testuser1", "admin")]
        [InlineData("testuser2", "user")]
        [InlineData("testuser3", "guest")]
        public void GenerateJwtToken_ShouldReturnValidToken(string username, string role)
        {
            // Act
            string token = JwtTokenGenerator.GenerateJwtToken(username, role);

            // Assert
            Assert.NotNull(token);
            Assert.NotEmpty(token);
        }

        [Theory]
        [InlineData("testuser1", "admin")]
        [InlineData("testuser2", "user")]
        [InlineData("testuser3", "guest")]
        public void GenerateJwtToken_ShouldContainUsernameClaim(string username, string role)
        {
            // Act
            string token = JwtTokenGenerator.GenerateJwtToken(username, role);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // Assert
            Assert.Contains(jwtToken.Claims, c => c.Type == ClaimTypes.Name && c.Value == username);
        }

        [Theory]
        [InlineData("testuser1", "admin")]
        [InlineData("testuser2", "user")]
        [InlineData("testuser3", "guest")]
        public void GenerateJwtToken_ShouldContainRoleClaim(string username, string role)
        {
            // Act
            string token = JwtTokenGenerator.GenerateJwtToken(username, role);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // Assert
            Assert.Contains(jwtToken.Claims, c => c.Type == ClaimTypes.Role && c.Value == role);
        }

        [Theory]
        [InlineData("testuser1", "admin")]
        [InlineData("testuser2", "user")]
        [InlineData("testuser3", "guest")]
        public void GenerateJwtToken_ShouldHaveValidExpiration(string username, string role)
        {
            // Act
            string token = JwtTokenGenerator.GenerateJwtToken(username, role);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // Assert
            Assert.True(jwtToken.ValidTo > DateTime.UtcNow);
        }
    }
}
