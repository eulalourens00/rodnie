using Rodnie.API.DTO.Requests.Groups;
using Rodnie.API.DTO.Responses.Groups;

namespace Rodnie.API.Services.Groups {
    public interface IGroupService {
        Task<GroupResponse> CreateAsync(CreateGroupRequest request, string creatorId);
        //Task<GroupResponse> UpdateAsync(UpdateGroupRequest request);

        Task<List<GroupResponse>> GetGroupsAsync(string userId);
    }
}
