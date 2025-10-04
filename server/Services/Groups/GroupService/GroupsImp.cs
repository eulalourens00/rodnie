using AutoMapper;
using Rodnie.API.Repositories.Groups;
using Rodnie.API.DTO.Requests.Groups;
using Rodnie.API.DTO.Responses.Groups;
using Rodnie.API.Models;
using Rodnie.API.Exceptions;

namespace Rodnie.API.Services.Groups.GroupService {
    public partial class GroupService : IGroupService {
        private readonly IGroupRepository repository;
        private readonly IMapper mapper;

        public GroupService(IGroupRepository repository, IMapper mapper) {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<GroupResponse> CreateAsync(CreateGroupRequest request, string creatorId) {
            var group = mapper.Map<Group>(request);
            group.owner_user_id = Guid.Parse(creatorId);

            var userGroups = await repository.GetGroupsAsync(creatorId);
            if (userGroups.Count >= 3) {
                throw new LimitExceededException("Достигнут лимит групп");
            }
            group = await repository.CreateAsync(group);
            return mapper.Map<GroupResponse>(group);
        }

        public async Task<List<GroupResponse>> GetGroupsAsync(string userId) {
            var groups = await repository.GetGroupsAsync(userId);
            return mapper.Map<List<GroupResponse>>(groups);
        }
    }
}
