using MongoDB.Bson;
using MongoDB.Driver;

namespace OurSolarSystemAPI.Repository.MongoDB 
{
    public class UserDtoResponse 
    {
        public required string Username { get; set; }
        public required ObjectId Id { get; set; }
    }
    public class UserRepositoryMongoDB 
    {
        private readonly IMongoCollection<UserDTOMongoDB> _users;

        public UserRepositoryMongoDB(MongoDbContext context)
        {
            _users = context.GetCollection<UserDTOMongoDB>("Users");
        }
        public async Task<UserDtoResponse> CreateUser(UserDTOMongoDB user) 
        {
            await _users.InsertOneAsync(user);
            return new UserDtoResponse
                {
                    Id = user.Id,
                    Username = user.Username
                };
        }

        public async Task<UserDTOMongoDB> GetUserWithPasswordAndSalt(string username) 
        {
            return await _users.Find(u => u.Username == username)
            .FirstOrDefaultAsync();

        }

        public async Task GetUserByUsername(string username) 
        {
            await _users.Find(u => u.Username == username)
            .Project(u => new UserDtoResponse
                {
                    Id = u.Id,
                    Username = u.Username
                })
            .FirstOrDefaultAsync();
        }

        public async Task GetUserById(ObjectId userId) 
        {
            await _users.Find(u => u.Id == userId)
            .Project(u => new UserDtoResponse
                {
                    Id = u.Id,
                    Username = u.Username
                })
            .FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteUser(ObjectId userId) 
        {
            var result = await _users.DeleteOneAsync(u => u.Id == userId);

            return result.DeletedCount == 1;
        }

        public async Task<Boolean> UpdatePassword(ObjectId userId, string newPassword, string newSalt) 
        {
            var result = await _users.UpdateOneAsync(
            u => u.Id == userId,             
            Builders<UserDTOMongoDB>.Update
            .Set(u => u.Password, newPassword)
            .Set(u => u.PasswordSalt, newSalt));

            return result.ModifiedCount == 1;
        }

        public async Task<Boolean> UpdateUsername(ObjectId userId, string newUsername) 
        {
            var result = await _users.UpdateOneAsync(
            u => u.Id == userId,             
            Builders<UserDTOMongoDB>.Update.Set(u => u.Username, newUsername));

            return result.ModifiedCount == 1;
        }
    }
}