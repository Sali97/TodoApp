using TaskAPI.Models;

namespace TaskAPI.Interfaces
{
    public interface ITodoRepository
    {
        Task<List<Todo>> GetAllAsync();
        Task<Todo> GetById(int id);
        Task AddAsync(Todo todo);
        Task DeleteAsync(int id);
        Task UpdateAsync(Todo todo);
    }
}
