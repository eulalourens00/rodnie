using Rodnie.API.Models;

namespace Rodnie.API.Services.JWT {
    public interface IJWTService {
        string GenerateToken(User user);
    }
}
