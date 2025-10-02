using Rodnie.API.Models;

namespace Rodnie.API.Repositories.Groups {
    public interface IGroupRepository {
        Task<List<Group>> GetUserGroupsAsync(string id);
        Task<Group> CreateAsync(Group group);
        Task<Group> UpdateAsync(Group group);
        void RemoveAsync(Group group);
    }
}
