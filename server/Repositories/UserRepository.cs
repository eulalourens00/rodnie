using Microsoft.EntityFrameworkCore;
using Rodnie.API.Data;
using Rodnie.API.Models;

namespace Rodnie.API.Repositories {
    public class UserRepository : IUserRepository {
        private readonly ApplicationDbContext context;

        public UserRepository(ApplicationDbContext context) {
            this.context = context;
        }

        public async Task<User?> GetByIdAsync(string id) {
            Guid userId = Guid.Parse(id);
            return await context.Users.FirstOrDefaultAsync(u => u.id == userId);
        }

        public async Task<User?> GetByUsernameAsync(string username) {
            return await context.Users.FirstOrDefaultAsync(u => u.username.ToLower() == username.ToLower());
        }

        public async Task<User> CreateAsync(User user) {
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }
    }
}
