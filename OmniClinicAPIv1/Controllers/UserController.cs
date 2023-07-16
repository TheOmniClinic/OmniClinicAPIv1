using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OmniClinicAPIv1.Models;
using OmniClinicAPIv1.Service;
using System;
using System.Collections.Generic;

namespace OmniClinicAPIv1.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // GET: api/users
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public IActionResult Create(User user)
        {
            var createdUser = _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, User user)
        {
            var userUpdated = _userService.UpdateUser(id, user);
            if (!userUpdated)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var userDeleted = _userService.DeleteUser(id);
            if (!userDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
