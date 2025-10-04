using Microsoft.EntityFrameworkCore;
using Rodnie.API.Models;

namespace Rodnie.API.Repositories.Groups.GroupRepository {
    public partial class GroupRepository : IGroupRepository {
        public async Task<List<Relation>> GetRelationsAsync(string id) {
            Guid userId = Guid.Parse(id);

            var relations = await context.Relations.Where(r => r.user_id == userId).ToListAsync();
            return relations;
        }

        public async Task<Relation> CreateRelationAsync(Relation relation) {
            context.Relations.Add(relation);
            await context.SaveChangesAsync();
            return relation;
        }
    }
}
