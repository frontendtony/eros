using Eros.Domain.Aggregates.Estates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eros.Persistence.Data.Estates.Configurations;

public class EstateUserEntityTypeConfiguration : IEntityTypeConfiguration<EstateUser>
{
    public void Configure(EntityTypeBuilder<EstateUser> builder)
    {
        builder.ToTable("EstateUsers");

        builder.HasKey(x => new { x.EstateId, x.UserId, x.RoleId });
        
        builder.Property(x => x.EstateId)
            .IsRequired();
        
        builder.Property(x => x.UserId)
            .IsRequired();
        
        builder.Property(x => x.RoleId)
            .IsRequired();
        
        builder.Property(x => x.ApartmentId);
        
        builder.Property(x => x.EstateUserType)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.Property(x => x.DeletedAt);

        builder.Property(x => x.CreatedBy)
            .IsRequired();
        
        builder.HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId);
        
        builder.HasOne(x => x.Estate)
            .WithMany()
            .HasForeignKey(x => x.EstateId);

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);
    }
}