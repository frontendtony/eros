using Eros.Domain.Aggregates.Users;
using Eros.Persistence.Data.Users.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Eros.Persistence.Data;

public class ErosReadContext : DbContext
{
    public ErosReadContext(DbContextOptions<ErosReadContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
    }
}