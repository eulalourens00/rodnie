using Microsoft.EntityFrameworkCore;
using Rodnie.API.Models;

namespace Rodnie.API.Data {
    public class ApplicationDbContext : DbContext{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }
        public DbSet<User> Users { get; set; }
    }
}
