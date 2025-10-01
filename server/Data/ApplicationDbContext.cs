using Microsoft.EntityFrameworkCore;
using Rodnie.API.Models;

namespace Rodnie.API.Data {
    public class ApplicationDbContext : DbContext{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Pin> Pins { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pin>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(p => p.owner_user_id)
                .OnDelete(DeleteBehavior.Cascade)
            ;

            modelBuilder.Entity<Group>()
                .ToTable("Groups")
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(g => g.owner_user_id)
                .OnDelete(DeleteBehavior.Cascade)
            ;

            modelBuilder.Entity<Relation>()
                .ToTable("Relations")
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.relation_user_id)
                .OnDelete(DeleteBehavior.Cascade)
            ;

            modelBuilder.Entity<Relation>()
                .ToTable("Relations")
                .HasOne<Group>()
                .WithMany()
                .HasForeignKey(r => r.relation_group_id)
                .OnDelete(DeleteBehavior.Cascade)
            ;
        }
    }
}
