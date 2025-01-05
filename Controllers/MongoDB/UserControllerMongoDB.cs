using Microsoft.AspNetCore.Mvc;
using OurSolarSystemAPI.Service.MongoDB;
using OurSolarSystemAPI.Models;

namespace OurSolarSystemAPI.Controllers.MongoDB 
{
    [ApiController]
    [Route("mongodb/api/planet")]
    public class UserControllerMongoDB : ControllerBase 
    {
        private readonly UserServiceMongoDB _userService;

        public UserControllerMongoDB(UserServiceMongoDB userService) 
        {
            _userService = userService;
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userInfo) 
        {
            var user = await _userService.CreateUser(userInfo.Username, userInfo.Password, userInfo.RepeatedPassword);
            return Ok(user);
        }


        [HttpPut("update-username")]
        public async Task<IActionResult> ChangeUsername(string oldUsername, string newUsername, string password, string repeatedPassword) 
        {
            bool isUpdated = await _userService.UpdateUsername(oldUsername, newUsername, password, repeatedPassword);

            return Ok(isUpdated);
        }

        [HttpPut("update-password")]
        public async Task<IActionResult> ChangePassword(string username, string oldPassword, string repeatedOldPassword, string newPassword, string repeatedNewPassword) 
        {
            bool isUpdated = await _userService.UpdatePassword(username, oldPassword, repeatedOldPassword, newPassword, repeatedNewPassword);

            return Ok(isUpdated);
        }

        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser(string username, string password, string repeatedPassword) 
        {
            bool isDeleted = await _userService.DeleteUser(username, password, repeatedPassword);

            return Ok(isDeleted);
        }

    }
}