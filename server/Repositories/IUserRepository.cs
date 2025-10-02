using Rodnie.API.Models;

namespace Rodnie.API.Repositories {
    public interface IUserRepository {
        // Check methods
        Task<User?> GetByIdAsync(string id);
        Task<User?> GetByUsernameAsync(string username);

        // Data methods
        Task<User> CreateAsync(User user);
    }
}
