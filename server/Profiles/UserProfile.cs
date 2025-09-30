using AutoMapper;

using Rodnie.API.Models;
using Rodnie.API.DTO.Requests;
using Rodnie.API.DTO.Responses;

namespace Rodnie.API.Profiles {
    public class UserProfile : Profile {
        public UserProfile() {
            CreateMap<CreateUserRequest, User>();
            CreateMap<User, UserResponse>();
        }
    }
}
