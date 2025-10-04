using AutoMapper;
using Rodnie.API.DTO.Requests.Groups;
using Rodnie.API.DTO.Responses.Groups;
using Rodnie.API.Models;

namespace Rodnie.API.Profiles {
    public class GroupProfile : Profile {
        public GroupProfile() {
            CreateMap<CreateGroupRequest, Group>();
            CreateMap<Group, GroupResponse>();
        }
    }
}
