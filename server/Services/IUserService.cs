using Rodnie.API.DTO.Requests;
using Rodnie.API.DTO.Requests.Auth;
using Rodnie.API.DTO.Responses;

namespace Rodnie.API.Services {
    public interface IUserService {
        Task<UserResponse> CreateAsync(CreateUserRequest request);
        Task<UserResponse> UsernameLoginAsync(UsernameLoginRequest request);
    }
}
