using Eros.Domain.Aggregates.Apartments;
using Eros.Domain.Aggregates.Buildings;
using Eros.Domain.Aggregates.Estates;
using Eros.Domain.Aggregates.Roles;
using Eros.Domain.Aggregates.Users;
using Eros.Persistence.Data.Apartments.Configurations;
using Eros.Persistence.Data.Buildings.Configurations;
using Eros.Persistence.Data.Estates.Configurations;
using Eros.Persistence.Data.Permissions.Configurations;
using Eros.Persistence.Data.Roles.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Eros.Persistence;

public class ErosDbContext(DbContextOptions<ErosDbContext> options) : IdentityUserContext<User>(options)
{
    public DbSet<Role> Roles { get; init; } = null!;
    public DbSet<Permission> Permissions { get; init; } = null!;
    public DbSet<Estate> Estates { get; init; } = null!;
    public DbSet<EstateRole> EstateRoles { get; init; } = null!;
    public DbSet<Building> Buildings { get; init; } = null!;
    public DbSet<BuildingType> BuildingTypes { get; init; } = null!;
    public DbSet<Apartment> Apartments { get; init; } = null!;
    public DbSet<ApartmentType> ApartmentTypes { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new RoleEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EstateEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EstateRoleEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new BuildingEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new BuildingTypeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ApartmentEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ApartmentTypeEntityTypeConfiguration());
    }
}
