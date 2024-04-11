using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Eros.Domain.Aggregates.Buildings;

namespace Eros.Persistence.Data.Buildings.Configurations;

public class BuildingEntityTypeConfiguration : IEntityTypeConfiguration<Building>
{
    public void Configure(EntityTypeBuilder<Building> builder)
    {
        builder.ToTable("Buildings");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Address)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasMaxLength(255);

        builder.Property(x => x.EstateId)
            .IsRequired();

        builder.HasOne(x => x.Estate)
            .WithMany(e => e.Buildings)
            .HasForeignKey(x => x.EstateId)
            .IsRequired();

        builder.Property(x => x.BuildingTypeId)
            .IsRequired();

        builder.HasOne(x => x.BuildingType)
            .WithMany()
            .HasForeignKey(x => x.BuildingTypeId);

        builder.HasMany(x => x.Apartments)
            .WithOne()
            .HasForeignKey(x => x.BuildingId);

        builder.Property(x => x.CreatedAt)
            .IsRequired();
    }
}
