using Neo4j.Driver;
using Neo4j.Driver.Mapping;
using OurSolarSystemAPI.Models;

namespace OurSolarSystemAPI.Repository.NEO4J 
{
    public class UserDtoResponse 
    {
        public required string Username { get; set; }
        public required int Id { get; set; }
    }

    public class UserDtoResponseWithPasswordAndSalt 
    {
        public required string Username { get; set; }
        public required int Id { get; set; }
        public required string Password { get; set; }
        public required string Salt { get; set; }
    }
    public class UserRepositoryNEO4J 
    {
        private readonly IDriver _driver;

        public UserRepositoryNEO4J(IDriver driver)
        {
            _driver = driver;
        }

        public Dictionary<string, object?> ConvertUserObjectToDict(UserEntity user)
        {
            return new Dictionary<string, object?>
            {
                { "username", user.Username },
                { "password", user.Password },
                { "salt", user.PasswordSalt }

            };
        }
        public async Task<UserDtoResponse> CreateUser(UserEntity user) 
        {
            await using var session = _driver.AsyncSession();

            Dictionary<string, object?> userAttributes = ConvertUserObjectToDict(user);
           

            var parameters = new Dictionary<string, object>
            {
                {"userAttributes", userAttributes}
            };

            var query = @"
                CREATE (u:User $userAttributes)
                RETURN id(u) AS id, u.username AS username";

            var result = await session.ExecuteWriteAsync(async tx =>
            {
                var cursor = await tx.RunAsync(query, parameters);
                var record = await cursor.SingleAsync() ?? throw new Exception("user node not created");

                return new UserDtoResponse
                {
                    Id = record["id"].As<int>(),
                    Username = record["username"].As<string>(),
                };
            });

            return result;
        }

        public async Task<UserDtoResponseWithPasswordAndSalt> GetUserWithPasswordAndSalt(string username) 
        {
            await using var session = _driver.AsyncSession();

            var query = @"
                MATCH (u:User {username: $username})
                RETURN id(u) AS id, u.username AS username, u.salt as salt, u.password as password"; 

      
            var parameters = new Dictionary<string, object>
            {
                { "username", username }
            };


            var result = await session.ExecuteReadAsync(async tx =>
            {
                var cursor = await tx.RunAsync(query, parameters);
                var record = await cursor.SingleAsync() ?? throw new Exception("No user found.");

                return new UserDtoResponseWithPasswordAndSalt
                {
                    Id = record["id"].As<int>(),
                    Username = record["username"].As<string>(),
                    Password = record["password"].As<string>(),
                    Salt = record["salt"].As<string>(),
                };
            });

            return result;

        }

        public async Task<UserDtoResponse?> GetUserByUsername(string username)
        {
            await using var session = _driver.AsyncSession();

            var query = @"
                MATCH (u:User {username: $username})
                RETURN id(u) AS id, u.username AS username"; 

      
            var parameters = new Dictionary<string, object>
            {
                { "username", username }
            };


            var result = await session.ExecuteReadAsync(async tx =>
            {
                var cursor = await tx.RunAsync(query, parameters);
                var record = await cursor.SingleAsync() ?? throw new Exception("No user found.");

                return new UserDtoResponse
                {
                    Id = record["id"].As<int>(),
                    Username = record["username"].As<string>(),
                };
            });

            return result;
        }

        public async Task<UserDtoResponse?> GetUserById(int userId)
        {
            await using var session = _driver.AsyncSession();

            var query = @"
                MATCH (u:User {username: $username})
                RETURN id(u) AS id, u.username AS username"; 

      
            var parameters = new Dictionary<string, object>
            {
                { "Id", userId }
            };


            var result = await session.ExecuteReadAsync(async tx =>
            {
                var cursor = await tx.RunAsync(query, parameters);
                var record = await cursor.SingleAsync() ?? throw new Exception("No user found.");

                return new UserDtoResponse
                {
                    Id = record["id"].As<int>(),
                    Username = record["username"].As<string>(),
                };
            });

            return result;
        }

        public async Task<bool> DeleteUserById(int userId)
        {
            await using var session = _driver.AsyncSession();

            var query = @"
                MATCH (u:User)
                WHERE id(u) = $userId
                DELETE u";

            var parameters = new Dictionary<string, object>
            {
                { "userId", userId }
            };

        
            var result = await session.ExecuteWriteAsync(async tx =>
            {
                var cursor = await tx.RunAsync(query, parameters);

                 var summary = await cursor.ConsumeAsync();
                return summary.Counters.NodesDeleted > 0;
            });

            return result;
        }

        public async Task<bool> UpdatePassword(int userId, string newPassword, string salt)
        {
            await using var session = _driver.AsyncSession();

           
            var query = @"
                MATCH (u:User)
                WHERE id(u) = $userId
                SET u.password = $newPassword, u.salt = $newSalt
                RETURN id(u) AS id, u.password AS password, u.salt AS salt";

            var parameters = new Dictionary<string, object>
            {
                { "userId", userId },
                { "newPassword", newPassword }
            };

            var result = await session.ExecuteWriteAsync(async tx =>
            {
                var cursor = await tx.RunAsync(query, parameters);
                var record = await cursor.SingleAsync();

                return record != null && record["password"].As<string>() == newPassword;
            });

            return result;
        }

        public async Task<bool> UpdateUsername(int userId, string newUsername)
        {
            await using var session = _driver.AsyncSession();

            var query = @"
                MATCH (u:User)
                WHERE id(u) = $userId
                SET u.username = $newUsername
                RETURN id(u) AS id, u.username AS username";

            var parameters = new Dictionary<string, object>
            {
                { "userId", userId },
                { "newUsername", newUsername }
            };

            var result = await session.ExecuteWriteAsync(async tx =>
            {
                var cursor = await tx.RunAsync(query, parameters);
                var record = await cursor.SingleAsync();

                return record != null && record["username"].As<string>() == newUsername;
            });

            return result;
        }
    }
}