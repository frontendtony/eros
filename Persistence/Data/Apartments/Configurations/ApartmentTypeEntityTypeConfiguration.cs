using Eros.Domain.Aggregates.Apartments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eros.Persistence.Data.Apartments.Configurations;

public class ApartmentTypeEntityTypeConfiguration : IEntityTypeConfiguration<ApartmentType>
{
    public void Configure(EntityTypeBuilder<ApartmentType> builder)
    {
        builder.ToTable("ApartmentTypes");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.Property(x => x.Description)
            .IsRequired(false);
    }
}