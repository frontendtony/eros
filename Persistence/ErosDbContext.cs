using Eros.Domain.Aggregates.Estates;
using Eros.Domain.Aggregates.Roles;
using Eros.Domain.Aggregates.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Eros.Persistence;

public class ErosDbContext : IdentityUserContext<User>
{
    public ErosDbContext(DbContextOptions<ErosDbContext> options) : base(options)
    {
    }

    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Permission> Permissions { get; set; } = null!;
    public DbSet<Estate> Estates { get; set; } = null!;
    public DbSet<EstateRole> EstateRoles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ErosDbContext).Assembly);
    }
}
