using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Contracts.Repository
{
    public interface IUserRepository
    {
        Task<List<User>> UsersAsync();
        Task<User> UserByIdAsync(int id);
        Task<User> UserByUsernameAsync(string username);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
    }
}
