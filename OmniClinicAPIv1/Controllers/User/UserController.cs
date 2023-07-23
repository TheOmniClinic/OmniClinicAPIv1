using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OmniClinicAPIv1.ContextDB;
using OmniClinicAPIv1.Models;
using OmniClinicAPIv1.Service.User;
using System;
using System.Collections.Generic;

namespace OmniClinicAPIv1.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> PostUser(User user)
        {
            await _userService.PostUser(user);
            return Ok(user.Token);
        }

        [Authorize]
        [HttpGet]
        public async Task<List<User>> GetUsers()
        {
            return await _userService.GetUsers();
        }
    }
}