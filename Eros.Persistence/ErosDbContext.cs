using Eros.Common.Constants;
using Eros.Domain;
using Eros.Domain.Aggregates.Apartments;
using Eros.Domain.Aggregates.Buildings;
using Eros.Domain.Aggregates.Estates;
using Eros.Domain.Aggregates.Invitations;
using Eros.Domain.Aggregates.Roles;
using Eros.Domain.Aggregates.Users;
using Eros.Domain.Aggregates.VisitorBookings;
using Eros.Persistence.Data.Apartments.Configurations;
using Eros.Persistence.Data.Buildings.Configurations;
using Eros.Persistence.Data.Estates.Configurations;
using Eros.Persistence.Data.Invitations;
using Eros.Persistence.Data.Permissions.Configurations;
using Eros.Persistence.Data.Roles.Configurations;
using Eros.Persistence.Data.Users.Configurations;
using Eros.Persistence.Data.VisitorBookings.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Eros.Persistence;

public class ErosDbContext(DbContextOptions<ErosDbContext> options) : IdentityUserContext<User, Guid>(options), IUnitOfWork
{
    public DbSet<Role> Roles { get; init; } = null!;
    public DbSet<Permission> Permissions { get; init; } = null!;
    public DbSet<Estate> Estates { get; init; } = null!;
    public DbSet<EstateRole> EstateRole { get; init; } = null!;
    public DbSet<EstateUser> EstateUser { get; init; } = null!;
    public DbSet<Building> Buildings { get; init; } = null!;
    public DbSet<BuildingType> BuildingTypes { get; init; } = null!;
    public DbSet<Apartment> Apartments { get; init; } = null!;
    public DbSet<ApartmentType> ApartmentTypes { get; init; } = null!;
    public DbSet<Invitation> Invitations { get; init; } = null!;
    public DbSet<VisitorBooking> VisitorBookings { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new RoleEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EstateEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new BuildingEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new BuildingTypeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ApartmentEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ApartmentTypeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new InvitationEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new VisitorBookingEntityTypeConfiguration());

        modelBuilder.Entity<Permission>().HasData(
            new Permission(Guid.Parse("8cfdbbb4-cb87-4d78-9806-c20a63b87530"), PermissionConstants.CreateEstate),
            new Permission(Guid.Parse("d542cd68-6d1a-4588-92c1-b35f550a2a1b"), PermissionConstants.UpdateEstate),
            new Permission(Guid.Parse("32e40dda-f579-4fd8-aeee-d157cedeb062"), PermissionConstants.DeleteEstate),
            new Permission(Guid.Parse("e45f233e-6dbd-4228-8b18-a23a66c1b18b"), PermissionConstants.ViewEstate),
            new Permission(Guid.Parse("b80d1e12-44bb-4457-a1ce-e6fbbbd74cec"), PermissionConstants.CreateBuilding),
            new Permission(Guid.Parse("2a84ccda-2adf-4215-8e2e-5bbd72d5216c"), PermissionConstants.UpdateBuilding),
            new Permission(Guid.Parse("bd848626-40c8-499a-b9f8-2886cf57d8c6"), PermissionConstants.DeleteBuilding),
            new Permission(Guid.Parse("af23db24-f6c0-4448-9f43-67288c4f5328"), PermissionConstants.ViewBuilding),
            new Permission(Guid.Parse("e300d634-8686-40b2-b396-6e0ac5cb0d09"), PermissionConstants.ListBuilding),
            new Permission(Guid.Parse("9765a47d-7e67-4d24-aa86-af372748ec7a"), PermissionConstants.CreateApartment),
            new Permission(Guid.Parse("61e201f7-7859-40b9-bbe8-715f29204291"), PermissionConstants.UpdateApartment),
            new Permission(Guid.Parse("1c8a89a0-b6fe-4efc-b0e3-12db170433fe"), PermissionConstants.DeleteApartment),
            new Permission(Guid.Parse("8b7a4964-46b3-4a03-a19e-86c35cb5b3cd"), PermissionConstants.ViewApartment),
            new Permission(Guid.Parse("2c67bdb4-7d9e-419a-816d-be29da2e837a"), PermissionConstants.ListApartment),
            new Permission(Guid.Parse("98d6ab3d-8a40-44bc-85d6-96c739813f9d"), PermissionConstants.CreateRole),
            new Permission(Guid.Parse("1a4b7beb-488b-4666-86d4-991895e0872b"), PermissionConstants.UpdateRole),
            new Permission(Guid.Parse("4c6e1ac5-b00e-41f5-89fa-c801578a9818"), PermissionConstants.DeleteRole),
            new Permission(Guid.Parse("ed64a073-1821-407c-add6-2e88f5d045e1"), PermissionConstants.ViewRole),
            new Permission(Guid.Parse("e0a2cb5c-28e2-4ee7-8fb9-a00a3e761e5e"), PermissionConstants.ListRole),
            new Permission(Guid.Parse("ff644b10-f762-403b-b4a9-a511fa0b6e08"), PermissionConstants.CreateVisitorBooking),
            new Permission(Guid.Parse("c08453b3-f11d-4fe3-ad43-b50b6cb47ec1"), PermissionConstants.AdmitVisitorBooking),
            new Permission(Guid.Parse("72571cf2-435b-440a-a5c3-d9fb84c550ba"), PermissionConstants.RejectVisitorBooking),
            new Permission(Guid.Parse("c7837520-c674-4dfa-bc4b-9d03e3753125"), PermissionConstants.ListVisitorBooking)
        );
    }
}
