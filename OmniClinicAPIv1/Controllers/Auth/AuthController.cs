using Microsoft.AspNetCore.Mvc;
using OmniClinicAPIv1.JWT.Services;
using OmniClinicAPIv1.Models;
using OmniClinicAPIv1.Service.User;
using System.Runtime.InteropServices.JavaScript;

namespace OmniClinicAPIv1.Controllers.Auth
{
    public class AuthController : Controller
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("api/v1/createauth")]
        public async Task<IActionResult> CreateAuthAsync(User user)
        {
            await _userService.PostUser(user);

            var token = TokenService.GenerateToken(user);
            return Ok(token);
        }

        [HttpPost]
        [Route("api/v1/updateauth")]
        public IActionResult UpdateAuth(string username, string password)
        {
            User user = new User();
            user.Name = username;

            if (username == "filipe" && password == "123456")
            {
                var token = TokenService.GenerateToken(new User());
                return Ok(token);
            }

            return BadRequest("username or password invalid");
        }
    }
}
