using Microsoft.EntityFrameworkCore;

namespace Laba.DTO
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Конфигурация связи many-to-many
            modelBuilder.Entity<User>()
                .HasMany(u => u.Projects)
                .WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserProjects",
                    j => j.HasOne<Project>().WithMany().HasForeignKey("ProjectId"),
                    j => j.HasOne<User>().WithMany().HasForeignKey("UserId")
                );
        }
    }
}
