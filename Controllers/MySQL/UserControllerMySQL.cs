using Microsoft.AspNetCore.Mvc;
using OurSolarSystemAPI.Models;
using OurSolarSystemAPI.Service.MySQL;

namespace OurSolarSystemAPI.Controllers.MySQL 
{
    [ApiController]
    [Route("mysql/api/user")]
    public class UserControllerMySQL : ControllerBase 
    {
        private readonly UserServiceMySQL _userService;

        public UserControllerMySQL(UserServiceMySQL userService) 
        {
            _userService = userService;
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userInfo) 
        {
            var user = await _userService.CreateUser(userInfo.Username, userInfo.Password, userInfo.RepeatedPassword);
            return Ok(user);
        }

        [HttpGet("get-user-by-username")]
        public async Task<IActionResult> RequestUserByUsername(string username, string password, string repeatedPassword) 
        {
            var user = await _userService.CreateUser(username, password, repeatedPassword);
            return Ok(user);
        }

        [HttpPut("update-username")]
        public async Task<IActionResult> UpdateUsername(string oldUsername, string newUsername, string password, string repeatedPassword) 
        {
            var user = await _userService.UpdateUsername(oldUsername, newUsername, password, repeatedPassword);
            return Ok(user);
        }

        [HttpPut("update-password")]
        public async Task<IActionResult> UpdatePassword(string username, string oldPassword, string repeatedOldPassword, string newPassword, string repeatedNewPassword) 
        {
            var user = await _userService.UpdatePassword(username, oldPassword, repeatedOldPassword, newPassword, repeatedNewPassword);
            return Ok(user);
        }

        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser(string username, string password, string repeatedPassword) 
        {
            var isDeleted = await _userService.DeleteUser(username, password, repeatedPassword);
            return Ok(isDeleted);
        }

    }
}