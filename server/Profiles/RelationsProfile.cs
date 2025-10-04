using AutoMapper;
using Rodnie.API.DTO.Requests.Groups;
using Rodnie.API.DTO.Responses.Groups;
using Rodnie.API.Models;

namespace Rodnie.API.Profiles {
    public class RelationsProfile : Profile {
        public RelationsProfile() {
            CreateMap<CreateRelationRequest, Relation>();
            CreateMap<Relation, RelationResponse>();
        }
    }
}
