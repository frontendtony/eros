using Eros.Domain.Aggregates.Invitations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eros.Persistence.Data.Invitations;

public class InvitationEntityTypeConfiguration : IEntityTypeConfiguration<Invitation>
{
    public void Configure(EntityTypeBuilder<Invitation> builder)
    {
        builder.ToTable("Invitations");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(x => x.CreatedBy)
            .IsRequired();

        builder.Property(x => x.EstateId)
            .IsRequired();

        builder.Property(x => x.RoleId)
            .IsRequired();

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.Expiration)
            .IsRequired();
    }
}
