using Dapper;
using System.Data.SQLite;
using TaskAPI.Interfaces;
using TaskAPI.Models;

namespace TaskAPI.Repositories
{
    public class TodoRepository: ITodoRepository
    {
        private readonly IConfiguration _configuration;
        public TodoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(_configuration.GetConnectionString("SqliteConnection"));
        }
        public async Task AddAsync(Todo todo)
        {
            using var connection = GetConnection();
            var T = await connection.ExecuteAsync("INSERT INTO tblTodos (Name, Desc, IsDone, CreatedBy, WaitingFor) VALUES (@Name, @Desc, @IsDone, @CreatedBy, @WaitingFor)", todo);
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = GetConnection();
            var T = await connection.ExecuteAsync("DELETE FROM tblTodos WHERE Id=@ID", new { ID = id });
        }

        public async Task<List<Todo>> GetAllAsync()
        {
            using var connection = GetConnection();
            var todos = await connection.QueryAsync<Todo>("SELECT * FROM tblTodos");
            return todos.ToList();
        }

        public async Task<Todo> GetById(int id)
        {
            using var connection = GetConnection();
            var todo = await connection.QueryFirstOrDefaultAsync<Todo>("SELECT * FROM tblTodos WHERE Id=@ID", new { ID = id });
            return todo;
        }

        public async Task UpdateAsync(Todo todo)
        {
            using var connection = GetConnection();
            var T = await connection.ExecuteAsync("UPDATE tblTodos SET Name=@Name, Desc=@Desc, IsDone=@IsDone, CreatedBy=@CreatedBy, WaitingFor=@WaitingFor WHERE Id=@Id", todo);
        }
    }
}
