using Eros.Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eros.Persistence.Data.Users.Configurations;

class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable("Users")
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder
            .Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(x => x.Avatar)
            .HasMaxLength(255);

        builder
            .Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(128);

        builder
            .Property(x => x.CreatedAt)
            .IsRequired();

        builder
            .Property(x => x.UpdatedAt);
    }
}
