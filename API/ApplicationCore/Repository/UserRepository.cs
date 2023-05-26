using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Contracts.Repository;
using TaskManagerAPI.Data;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Repository
{
    public class UserRepository : IUserRepository
    {       
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<User>> UsersAsync()
        {
            var users = await _context.Users.ToListAsync();

            return users;
        }

        public async Task<User> UserByIdAsync(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);

            if (user == null)
                throw new NullReferenceException("User does not exist!");

            return user;
        }

        public async Task<User> UserByUsernameAsync(string username)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == username);
            if (user == null)
                throw new NullReferenceException("User does not exist!");

            return user;
        }

        public async Task UpdateAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await SaveAllAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await UserByIdAsync(id);

            _context.Users.Remove(user);
            await SaveAllAsync();
        }

        private async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
