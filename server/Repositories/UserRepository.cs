using Microsoft.EntityFrameworkCore;
using Rodnie.API.Data;
using Rodnie.API.Models;

namespace Rodnie.API.Repositories {
    public class UserRepository : IUserRepository {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<User?> GetByUsernameAsync(string username) {
            return await _context.Users.FirstOrDefaultAsync(u => u.username.ToLower() == username.ToLower());
        }

        public async Task<User> CreateAsync(User user) {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
