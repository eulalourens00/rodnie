using Rodnie.API.DTO.Requests.Groups;
using Rodnie.API.DTO.Responses.Groups;

namespace Rodnie.API.Services.Groups.GroupService {
    public partial class GroupService : IGroupService {
        public async Task<List<RelationResponse>> GetRelationsAsync(string userId) {
            var relations = await repository.GetRelationsAsync(userId);
            return mapper.Map<List<RelationResponse>>(relations);
        }

        public async Task<RelationResponse> CreateRelationAsync(CreateRelationRequest request, string creatorId) {

        }
    }
}
