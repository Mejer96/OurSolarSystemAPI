using System.Text.RegularExpressions;
using OurSolarSystemAPI.Exceptions;

namespace OurSolarSystemAPI.Utility 
{
    public static class UserValidator 
    {
        public static bool ValidateUsername(string username) 
        {
            string pattern = @"^[a-zA-Z][a-zA-Z0-9_-]{5,14}$";

            if (Regex.IsMatch(username, pattern)) return true;
            else throw new BadRequest("invalid username");
        }

        public static bool ValidatePassword(string password) 
        {
            string pattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()\-_=+{}[\]|;:'"",<.>/?]).{8,30}$";

            if  (Regex.IsMatch(password, pattern)) return true;
            else throw new BadRequest("invalid username");
        }
    }
}