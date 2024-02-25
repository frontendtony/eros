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
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.EstateId)
            .IsRequired();

        builder.HasOne(x => x.Estate)
            .WithMany()
            .HasForeignKey(x => x.EstateId);

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

        builder.HasIndex(x => new { x.Name, x.EstateId })
            .IsUnique();
    }
}
