using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OmniClinicAPIv1.Models;

namespace OmniClinicAPIv1.ContextDB
{
    public class MongoDBSettings
    {
        private readonly IMongoCollection<User> _userCollection;

        public MongoDBSettings(IOptions<UserContext> mongoDBSettings)
        {
            var mongoClient = new MongoClient(mongoDBSettings.Value.ConnectionString);
            var mongoDataBase = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);

            _userCollection = mongoDataBase.GetCollection<User>(mongoDBSettings.Value.CollectionName);
        }

        public async Task<List<User>> GetAsync() => await _userCollection.Find(x => true).ToListAsync();

        public async Task<User> GetAsync(string id) => await _userCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(User user) => await _userCollection.InsertOneAsync(user);

        public async Task UpdateAsync(string id, User user) => await _userCollection.ReplaceOneAsync(x => x.Id == id, user);

        public async Task RemoveAsync(string id) => await _userCollection.DeleteOneAsync(x => x.Id == id);
    }
}