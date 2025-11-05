using TaskAPI.Models;

namespace TaskAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetById(int id);
        Task AddAsync(User user);
        Task DeleteAsync(int id);
        Task UpdateAsync(int oldId, User user);
    }
}
