using AutoMapper;
using Crypto = BCrypt.Net.BCrypt;

using Rodnie.API.Models;
using Rodnie.API.DTO.Requests;
using Rodnie.API.DTO.Responses;
using Rodnie.API.Repositories;
using Rodnie.API.DTO.Requests.Auth;
using Rodnie.API.Exceptions;

namespace Rodnie.API.Services {
    public class UserService : IUserService {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper) {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserResponse> CreateAsync(CreateUserRequest request) {
            var usernameCheck = await _repository.GetByUsernameAsync(request.Username);
            if (usernameCheck != null) {
                throw new InvalidOperationException("User with this username already exists");
            }

            var user = _mapper.Map<User>(request);
            user.password = Crypto.HashPassword(request.Password, workFactor: 12);

            var created = await _repository.CreateAsync(user);
            return _mapper.Map<UserResponse>(created);
        }

        public async Task<UserResponse> UsernameLoginAsync(UsernameLoginRequest request) {
            var user = await _repository.GetByUsernameAsync(request.Username); // опционально, для безопастности можно удалить
            if (user == null) {
                throw new RodnieNotFoundException("User not found");
            }

            if (!Crypto.Verify(request.Password, user.password)) {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            //user.updated_at = DateTime.UtcNow; реализовать позже

            return _mapper.Map<UserResponse>(user);
        }
    }
}
