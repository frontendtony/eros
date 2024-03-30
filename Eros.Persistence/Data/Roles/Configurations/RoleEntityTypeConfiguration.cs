using Eros.Domain.Aggregates.Estates;
using Eros.Domain.Aggregates.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eros.Persistence.Data.Roles.Configurations;

public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnName("Description")
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("UpdatedAt")
            .IsRequired();

        builder.Property(x => x.IsShared)
            .HasColumnName("IsShared")
            .IsRequired();

        builder.HasMany(x => x.Permissions)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "RolePermission",
                x => x.HasOne<Permission>().WithMany().HasForeignKey("PermissionId"),
                x => x.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                x =>
                {
                    x.HasKey("RoleId", "PermissionId");
                    x.ToTable("RolePermissions", "roles");
                }
            );

        builder.HasMany(x => x.Estates)
            .WithMany()
            .UsingEntity<EstateRole>();
    }
}
