using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OmniClinicAPIv1.ContextDB;
using OmniClinicAPIv1.Models;
using System;
using System.Collections.Generic;

namespace OmniClinicAPIv1.Controllers
{
    [ApiController] 
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly MongoDBSettings _userMongo;

        public UserController(MongoDBSettings mongoDBSettings)
        {
            _userMongo = mongoDBSettings;
        }

        [HttpGet]
        public async Task<List<User>> GetUsers()
            => await _userMongo.GetAsync();

        [HttpPost]
        public async Task<User> PostUser(User user)
        {
            await _userMongo.CreateAsync(user);

            return user;
        }

    }
}
