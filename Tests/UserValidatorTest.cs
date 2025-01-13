using OurSolarSystemAPI.Utility;
using OurSolarSystemAPI.Exceptions;
using Xunit;

namespace OurSolarSystemTest
{
    public class UserValidatorTest
    {
        [Theory]
        [InlineData("ValidUser1")]
        [InlineData("User_123")]
        [InlineData("User-123")]
        public void ValidateUsername_ValidUsernames_ReturnsTrue(string username)
        {
            bool result = UserValidator.ValidateUsername(username);
            Assert.True(result);
        }

        [Theory]
        [InlineData("1InvalidUser")]
        [InlineData("Invalid!User")]
        [InlineData("short")]
        [InlineData("thisusernameiswaytoolongtobevalid")]
        public void ValidateUsername_InvalidUsernames_ThrowsBadRequest(string username)
        {
            Assert.Throws<BadRequest>(() => UserValidator.ValidateUsername(username));
        }

        [Theory]
        [InlineData("ValidPass1!")]
        [InlineData("Another$Pass2")]
        [InlineData("StrongPass#3")]
        public void ValidatePassword_ValidPasswords_ReturnsTrue(string password)
        {
            bool result = UserValidator.ValidatePassword(password);
            Assert.True(result);
        }

        [Theory]
        [InlineData("weakpass")]
        [InlineData("NoSpecialChar1")]
        [InlineData("NoNumber!")]
        [InlineData("SHORT1!")]
        [InlineData("thispasswordiswaytoolongtobevalidandshouldfail1!")]
        public void ValidatePassword_InvalidPasswords_ThrowsBadRequest(string password)
        {
            Assert.Throws<BadRequest>(() => UserValidator.ValidatePassword(password));
        }
    }
}