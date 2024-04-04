using Eros.Domain.Aggregates.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eros.Persistence.Data.Permissions.Configurations;

public class PermissionEntityTypeConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .IsRequired()
            .HasMaxLength(50);

        builder.HasMany(x => x.Roles)
            .WithMany(x => x.Permissions)
            .UsingEntity<RolePermission>();
        
        builder.HasIndex(x => x.Name)
            .IsUnique();
    }
}
