using AutoMapper;
using Crypto = BCrypt.Net.BCrypt;

using Rodnie.API.Models;
using Rodnie.API.DTO.Requests;
using Rodnie.API.DTO.Responses;
using Rodnie.API.Repositories;
using Rodnie.API.DTO.Requests.Auth;
using Rodnie.API.Exceptions;
using Rodnie.API.Services.JWT;

namespace Rodnie.API.Services {
    public class UserService : IUserService {
        private readonly IUserRepository repository;
        private readonly IJWTService jwt;
        private readonly IMapper mapper;

        public UserService(IUserRepository repository, IJWTService jwt, IMapper mapper) {
            this.repository = repository;
            this.jwt = jwt;
            this.mapper = mapper;
        }

        public async Task<UserResponse> CreateAsync(CreateUserRequest request) {
            var usernameCheck = await repository.GetByUsernameAsync(request.Username);
            if (usernameCheck != null) {
                throw new InvalidOperationException("User with this username already exists");
            }

            var user = mapper.Map<User>(request);
            user.password = Crypto.HashPassword(request.Password, workFactor: 12);

            var createdUser = await repository.CreateAsync(user);
            var response = mapper.Map<UserResponse>(createdUser);
            response.token = jwt.GenerateToken(createdUser);
            return response;
        }

        public async Task<UserResponse> UsernameLoginAsync(UsernameLoginRequest request) {
            var user = await repository.GetByUsernameAsync(request.Username); // опционально, для безопастности можно удалить
            if (user == null) {
                throw new NotFoundException("User not found");
            }

            if (!Crypto.Verify(request.Password, user.password)) {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            //user.updated_at = DateTime.UtcNow; реализовать позже

            var response = mapper.Map<UserResponse>(user);
            response.token = jwt.GenerateToken(user);
            return response;
        }
    }
}
