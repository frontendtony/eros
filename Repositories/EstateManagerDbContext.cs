using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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
    }
}