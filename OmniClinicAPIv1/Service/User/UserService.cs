using OmniClinicAPIv1.ContextDB;
using OmniClinicAPIv1.Models;

namespace OmniClinicAPIv1.Service.User
{
    public class UserService
    {
        private readonly MongoDBSettings _userMongo;

        public UserService(MongoDBSettings mongoDBSettings)
        {
            _userMongo = mongoDBSettings;
        }

        public async Task<List<Models.User>> GetUsers()
        {
            return await _userMongo.GetAsync();
        }

        public async Task<Models.User> PostUser(Models.User user)
        {
            await _userMongo.CreateAsync(user);

            return user;
        }
    }
        
}
