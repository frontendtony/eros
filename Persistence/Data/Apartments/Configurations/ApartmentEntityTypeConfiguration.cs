using Eros.Domain.Aggregates.Apartments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eros.Persistence.Data.Apartments.Configurations;

public class ApartmentEntityTypeConfiguration : IEntityTypeConfiguration<Apartment>
{
    public void Configure(EntityTypeBuilder<Apartment> builder)
    {
        builder.ToTable("Apartments");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.BuildingId)
            .IsRequired();

        builder.HasOne(x => x.Building)
            .WithMany(x => x.Apartments)
            .HasForeignKey(x => x.BuildingId);

        builder.Property(x => x.ApartmentTypeId)
            .IsRequired();

        builder.HasOne(x => x.ApartmentType)
            .WithMany()
            .HasForeignKey(x => x.ApartmentTypeId);

        builder.Property(x => x.NumberOfRooms)
            .IsRequired();

        builder.HasIndex(x => new { x.Name, x.BuildingId })
            .IsUnique();
    }
}
