using Eros.Domain.Aggregates.Estates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eros.Persistence.Data.Estates;

public class EstateEntityTypeConfiguration : IEntityTypeConfiguration<Estate>
{
    public void Configure(EntityTypeBuilder<Estate> builder)
    {
        builder.ToTable("Estates");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);

        builder.Property(x => x.DeletedAt)
            .IsRequired(false);

        builder.Property(x => x.CreatedBy)
            .IsRequired();

        builder.HasMany(x => x.Roles)
            .WithMany()
            .UsingEntity<EstateRole>();
    }
}
