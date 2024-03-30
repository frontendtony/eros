using Eros.Domain.Aggregates.Estates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eros.Persistence.Data.Estates.Configurations;

public class EstateRoleEntityTypeConfiguration : IEntityTypeConfiguration<EstateRole>
{
    public void Configure(EntityTypeBuilder<EstateRole> builder)
    {
        builder.ToTable("EstateRoles");

        builder.HasKey(x => new { x.EstateId, x.RoleId });

        builder.HasOne(x => x.Estate)
            .WithMany()
            .HasForeignKey(x => x.EstateId);

        builder.HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId);
    }
}
