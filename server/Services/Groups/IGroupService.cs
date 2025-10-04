using Rodnie.API.DTO.Requests.Groups;
using Rodnie.API.DTO.Responses.Groups;

namespace Rodnie.API.Services.Groups {
    public interface IGroupService {
        /* 
         * Groups
        */

        // ~
        Task<List<GroupResponse>> GetGroupsAsync(string userId);

        // CRUD
        Task<GroupResponse> CreateAsync(CreateGroupRequest request, string creatorId);
        //Task<GroupResponse> UpdateAsync(UpdateGroupRequest request);

        /*
         * Relations
        */

        // ~ 
        Task<List<RelationResponse>> GetRelationsAsync(string userId);

        // CRUD 
        Task<RelationResponse> CreateRelationAsync(CreateRelationRequest request, string creatorId);
    }
}
