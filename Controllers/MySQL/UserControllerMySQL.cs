using Microsoft.AspNetCore.Mvc;
using OurSolarSystemAPI.Models;
using OurSolarSystemAPI.Service.MySQL;
using OurSolarSystemAPI.Controllers.ExceptionHandler;
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
            try 
            {
                var user = await _userService.CreateUser(userInfo.Username, userInfo.Password, userInfo.RepeatedPassword);
                return Created("", user);
            } 
            catch (Exception exception)
            {
                return ControllerExceptionHandler.HandleException(exception, this);
            }
        }

        [HttpPost("authenticate-user")]
        public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateUserDto userInfo) 
        {
            try 
            {
                var isAuthenticated = await _userService.GetUserByUsernameAndPassword(userInfo.Username, userInfo.Password);
                return Ok();
            }
            catch (Exception exception)
            {
                return ControllerExceptionHandler.HandleException(exception, this);
            }
        }

        [HttpGet("get-user-by-username")]
        public async Task<IActionResult> RequestUserByUsername(string username) 
        {
            try 
            {
                var user = await _userService.GetUserByUsername(username);
                return Ok(user);
            }
            catch (Exception exception)
            {
                return ControllerExceptionHandler.HandleException(exception, this);
            }
        }

        [HttpPut("update-username")]
        public async Task<IActionResult> UpdateUsername(string oldUsername, string newUsername, string password, string repeatedPassword) 
        {
            try 
            {
                var user = await _userService.UpdateUsername(oldUsername, newUsername, password, repeatedPassword);
                return Ok(user);
            }
            catch (Exception exception)
            {
                return ControllerExceptionHandler.HandleException(exception, this);
            }
        }

        [HttpPut("update-password")]
        public async Task<IActionResult> UpdatePassword(string username, string oldPassword, string repeatedOldPassword, string newPassword, string repeatedNewPassword) 
        {
            try 
            {
                var user = await _userService.UpdatePassword(username, oldPassword, repeatedOldPassword, newPassword, repeatedNewPassword);
                return Ok(user);
            }
            catch (Exception exception)
            {
                return ControllerExceptionHandler.HandleException(exception, this);
            }
        }

        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser(string username, string password, string repeatedPassword) 
        {
            try 
            {
                var isDeleted = await _userService.DeleteUser(username, password, repeatedPassword);
                return Ok(isDeleted);
            }
            catch (Exception exception)
            {
                return ControllerExceptionHandler.HandleException(exception, this);
            }
        }
    }
}