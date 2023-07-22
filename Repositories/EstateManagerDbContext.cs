using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EstateManager.Constants;

namespace EstateManager.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using EstateManager.Entities;

    public class EstateManagerDbContext : IdentityUserContext<ApplicationUser>
    {
        public EstateManagerDbContext(DbContextOptions<EstateManagerDbContext> options) : base(options)
        {
        }

        public DbSet<Estate> Estates { get; set; } = null!;
        public DbSet<EstateBuilding> EstateBuildings { get; set; } = null!;
        public DbSet<EstatePermission> EstatePermissions { get; set; } = null!;
        public DbSet<EstateRole> EstateRoles { get; set; } = null!;
        public DbSet<EstateRolePermission> EstateRolePermissions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}