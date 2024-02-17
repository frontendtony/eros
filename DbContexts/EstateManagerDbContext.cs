using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EstateManager.Entities;
using Eros.Domain.Aggregates.Users;

namespace EstateManager.DbContexts;

public class EstateManagerDbContext : IdentityUserContext<User>
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