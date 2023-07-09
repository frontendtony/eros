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
        public DbSet<Permission> Permissions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Permission>().HasData(
                new Permission { Id = Guid.NewGuid(), Name = PermissionConstants.CreateEstate, Description = "Permission to create an estate" },
                new Permission { Id = Guid.NewGuid(), Name = PermissionConstants.UpdateEstate, Description = "Permission to update an estate" },
                new Permission { Id = Guid.NewGuid(), Name = PermissionConstants.DeleteEstate, Description = "Permission to delete an estate" },
                new Permission { Id = Guid.NewGuid(), Name = PermissionConstants.CreateEstateBuilding, Description = "Permission to create an estate building" },
                new Permission { Id = Guid.NewGuid(), Name = PermissionConstants.UpdateEstateBuilding, Description = "Permission to update an estate building" },
                new Permission { Id = Guid.NewGuid(), Name = PermissionConstants.DeleteEstateBuilding, Description = "Permission to delete an estate building" }
            );
        }
    }
}