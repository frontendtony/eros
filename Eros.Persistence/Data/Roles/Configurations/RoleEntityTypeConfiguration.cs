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
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnName("Description")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("UpdatedAt");

        builder.Property(x => x.IsShared)
            .HasColumnName("IsShared")
            .IsRequired();

        builder.HasMany(x => x.Permissions)
            .WithMany(x => x.Roles)
            .UsingEntity<RolePermission>();

        builder.HasMany(x => x.Estates)
            .WithMany()
            .UsingEntity<EstateRole>();
    }
}
