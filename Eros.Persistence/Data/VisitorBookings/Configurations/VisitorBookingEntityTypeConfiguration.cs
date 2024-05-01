using Eros.Domain.Aggregates.VisitorBookings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eros.Persistence.Data.VisitorBookings.Configurations;

public class VisitorBookingEntityTypeConfiguration : IEntityTypeConfiguration<VisitorBooking>
{
    public void Configure(EntityTypeBuilder<VisitorBooking> builder)
    {
        builder.ToTable("VisitorBookings");

        builder.HasKey(vb => vb.Id);

        builder.Property(vb => vb.EstateId)
            .IsRequired();

        builder.Property(vb => vb.CreatedBy)
            .IsRequired();

        builder.Property(vb => vb.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(vb => vb.Gender)
            .IsRequired();

        builder.Property(vb => vb.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(vb => vb.Purpose)
            .HasMaxLength(200);

        builder.Property(vb => vb.RejectionReason)
            .HasMaxLength(200);

        builder.Property(vb => vb.Status)
            .IsRequired();

        builder.Property(vb => vb.CreatedAt)
            .IsRequired();
    }
}
