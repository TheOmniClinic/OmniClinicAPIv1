using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.IdentityModel.Tokens;
using OmniClinicAPIv1.ContextDB;
using OmniClinicAPIv1.JWT;
using OmniClinicAPIv1.JWT.Interfaces;
using OmniClinicAPIv1.JWT.Services;
using OmniClinicAPIv1.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OmniClinicAPIv1.Service.User
{
    public class UserService
    {
        private readonly MongoDBSettings _userMongo;
        private readonly ITokenService _tokenService;

        public UserService(MongoDBSettings mongoDBSettings, ITokenService tokenService)
        {
            _userMongo = mongoDBSettings;
            _tokenService = tokenService;
        }

        public async Task<List<Models.User>> GetUsers()
        {
            return await _userMongo.GetAsync();
        }

        public async Task<Models.User> PostUser(Models.User user)
        {
            await _userMongo.CreateAsync(user);
            var token = _tokenService.GenerateToken(user);

            user.Token = token;
            await RecordTokenAsync(user);
            return user;
        }

        internal async Task Auth(Models.User user)
        {
            try
            {
                bool isValid = await ValidateUser(user);
                
                if (isValid)
                {
                    var token = _tokenService.GenerateToken(user);
                    user.Token = token; 
                    await RecordTokenAsync(user);
                }
                else
                {
                    throw new Exception("Email or password is invalid.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        internal async Task<bool> ValidateUser(Models.User user)
        {
            var userDb = await _userMongo.GetAsyncByEmail(user.Email);

            if (userDb == null)
            {
                return false;
            }

            user.Id = userDb.Id;

            if (user.Email == userDb.Email && user.Password == userDb.Password)
            {
                return true;
            }

            return false;
        }

        internal async Task RecordTokenAsync(Models.User user)
        {
            await _userMongo.UpdateAsync(user.Id, user);
        }
    }
}

