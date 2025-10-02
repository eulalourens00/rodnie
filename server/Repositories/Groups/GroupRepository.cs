using Microsoft.EntityFrameworkCore;
using Rodnie.API.Data;
using Rodnie.API.Models;

namespace Rodnie.API.Repositories.Groups {
    public class GroupRepository : IGroupRepository {
        public readonly ApplicationDbContext context;

        public GroupRepository(ApplicationDbContext context) {
            this.context = context;
        }

        public async Task<List<Group>> GetUserGroupsAsync(string id) {
            Guid userId = Guid.Parse(id);
            return await context.Groups.Where(g => g.owner_user_id == userId).ToListAsync();
        }

        public async Task<Group> CreateAsync(Group group) {
            context.Groups.Add(group);
            await context.SaveChangesAsync();
            return group;
        }

        public async Task<Group> UpdateAsync(Group group) {
            context.Groups.Update(group);
            await context.SaveChangesAsync();
            return group;
        }

        public async void RemoveAsync(Group group) {
            context.Groups.Remove(group);
            await context.SaveChangesAsync();
        }
    }
}
