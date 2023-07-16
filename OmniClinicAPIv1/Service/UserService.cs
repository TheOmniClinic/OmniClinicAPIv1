using MongoDB.Driver;
using OmniClinicAPIv1.Models;

namespace OmniClinicAPIv1.Service
{
    public class UserService
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UserService(IMongoDatabase database)
        {
            _usersCollection = database.GetCollection<User>("users");
        }

        public List<User> GetAllUsers()
        {
            return _usersCollection.Find(FilterDefinition<User>.Empty).ToList();
        }

        public User GetUserById(int id)
        {
            return _usersCollection.Find(u => u.Id == id).FirstOrDefault();
        }

        public User CreateUser(User user)
        {
            _usersCollection.InsertOne(user);
            return user;
        }

        public bool UpdateUser(int id, User user)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            var update = Builders<User>.Update.Set(u => u.Name, user.Name);
            var result = _usersCollection.UpdateOne(filter, update);
            return result.ModifiedCount > 0;
        }

        public bool DeleteUser(int id)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            var result = _usersCollection.DeleteOne(filter);
            return result.DeletedCount > 0;
        }
    }
}