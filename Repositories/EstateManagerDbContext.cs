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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EstatePermission>().HasData(
                new EstatePermission { Id = Guid.NewGuid(), Name = PermissionConstants.CreateEstate, Description = "Permission to create an estate" },
                new EstatePermission { Id = Guid.NewGuid(), Name = PermissionConstants.UpdateEstate, Description = "Permission to update an estate" },
                new EstatePermission { Id = Guid.NewGuid(), Name = PermissionConstants.DeleteEstate, Description = "Permission to delete an estate" },
                new EstatePermission { Id = Guid.NewGuid(), Name = PermissionConstants.CreateEstateBuilding, Description = "Permission to create an estate building" },
                new EstatePermission { Id = Guid.NewGuid(), Name = PermissionConstants.UpdateEstateBuilding, Description = "Permission to update an estate building" },
                new EstatePermission { Id = Guid.NewGuid(), Name = PermissionConstants.DeleteEstateBuilding, Description = "Permission to delete an estate building" }
            );
        }
    }
}