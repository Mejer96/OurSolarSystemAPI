using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using OurSolarSystemAPI.Exceptions;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace OurSolarSystemAPI.Controllers.ExceptionHandler 
{
    public static class ControllerExceptionHandler
    {
        public static IActionResult HandleException(Exception exception, ControllerBase controller) 
        {
             
                return exception switch
            {
                AuthenticationFailed => controller.UnprocessableEntity(new { error = exception.Message }), // 422
                ArgumentException => controller.BadRequest(new { error = exception.Message }), // 400
                EntityNotFound => controller.NotFound(new { error = exception.Message}), // 404
                BadRequest => controller.BadRequest(new { error = exception.Message}), // 400
                PasswordsNotMatching => controller.UnprocessableEntity(new { error = exception.Message}), // 422
                InvalidPassword => controller.UnprocessableEntity(new { error = exception.Message}), // 422
                DbUpdateException dbEx =>
                dbEx.InnerException is MySqlException mysqlEx && mysqlEx.Number == 1062
                ? controller.UnprocessableEntity(new { error = "Duplicate entry. The username already exists." }) // 422 for duplicate entry
                : controller.StatusCode(500, new { error = "Something went wrong" }),
                _ => controller.StatusCode(500, "Something went wrong")
            };

        }

    }
}