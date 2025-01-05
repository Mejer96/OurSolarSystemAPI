using Microsoft.AspNetCore.Mvc;
using OurSolarSystemAPI.Service.NEO4J;
using OurSolarSystemAPI.Models;

namespace OurSolarSystemAPI.Controllers.NEO4J
{
    [ApiController]
    [Route("neo4j/api/user")]
    public class UserControllerNEO4J : ControllerBase 
    {
        private readonly UserServiceNEO4J _userService;

        public UserControllerNEO4J(UserServiceNEO4J userService) 
        {
            _userService = userService;
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userInfo) 
        {
            var user = await _userService.CreateUser(userInfo.Username, userInfo.Password, userInfo.RepeatedPassword);
            return Ok(user);
        }


        [HttpPut("get-by-username")]
        public async Task<IActionResult> RequestByUsername(string username) 
        {
            var user = await _userService.GetUserByUsername(username);

            return Ok(user);
        }

        [HttpPut("update-username")]
        public async Task<IActionResult> UpdateUsername(string oldUsername, string newUsername, string password, string repeatedPassword) 
        {
            bool isUpdated = await _userService.UpdateUsername(oldUsername, newUsername, password, repeatedPassword);

            return Ok(isUpdated);
        }

        [HttpPut("update-password")]
        public async Task<IActionResult> UpdatePassword(string username, string oldPassword, string repeatedOldPassword, string newPassword, string repeatedNewPassword) 
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