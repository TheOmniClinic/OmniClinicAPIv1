using Microsoft.AspNetCore.Mvc;
using OmniClinicAPIv1.JWT.Services;
using OmniClinicAPIv1.Models;
using OmniClinicAPIv1.Service.User;
using System.Runtime.InteropServices.JavaScript;

namespace OmniClinicAPIv1.Controllers.Auth
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : Controller
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Auth(User user)
        {
            try
            {
                _userService.Auth(user);
                return Ok("Successful login");

            }
            catch (Exception)
            {
                throw new Exception("There was an error to login");
            }
        }
    }
}