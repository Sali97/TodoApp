using Dapper;
using System.Data.SQLite;
using TaskAPI.Interfaces;
using TaskAPI.Models;

namespace TaskAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(_configuration.GetConnectionString("SqliteConnection"));
        }
        public async Task AddAsync(User user)
        {
            using var connection = GetConnection();
            var U = await connection.ExecuteAsync("INSERT INTO tblUser (Id, Name, LevelId) VALUES (@Id, @Name, @LevelId)", user);
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = GetConnection();
            var U = await connection.ExecuteAsync("DELETE FROM tblUser WHERE Id=@ID", new {ID=id});
        }

        public async Task<List<User>> GetAllAsync()
        {
            using var connection = GetConnection();
            var users = await connection.QueryAsync<User>("SELECT * FROM tblUser");
            return users.ToList();
        }

        public async Task<User> GetById(int id)
        {
            using var connection = GetConnection();
            var user = await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM tblUser WHERE Id=@ID", new {ID=id});
            return user;
        }

        public async Task UpdateAsync(int oldId, User user)
        {
            user.Id = oldId; //Jelenleg ez csak teszt így, nem történik meg a frissítés - feltehetően mert alapvetően az új felhasználó ID-ját keresi az SQL.
            using var connection = GetConnection();
            var U = await connection.ExecuteAsync("UPDATE tblUser SET Name=@Name,LevelId=@LevelId WHERE Id=@Id", user);
        }
    }
}
