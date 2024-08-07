using Eros.Domain;
using Eros.Domain.Aggregates.Apartments;
using Eros.Domain.Aggregates.Buildings;
using Eros.Domain.Aggregates.Estates;
using Eros.Domain.Aggregates.Invitations;
using Eros.Domain.Aggregates.Roles;
using Eros.Domain.Aggregates.Users;
using Eros.Domain.Aggregates.VisitorBookings;
using Eros.Persistence.Data.Apartments.Repositories;
using Eros.Persistence.Data.Buildings.Repositories;
using Eros.Persistence.Data.Estates.Repositories;
using Eros.Persistence.Data.Invitations;
using Eros.Persistence.Data.Permissions.Repositories;
using Eros.Persistence.Data.Roles.Repositories;
using Eros.Persistence.Data.Users.Repositories;
using Eros.Persistence.Data.VisitorBookings.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eros.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ErosDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("Eros.Persistence")
            );
        });

        services.AddScoped<IUserReadRepository, UserReadRepository>();
        services.AddScoped<IUserWriteRepository, UserWriteRepository>();
        services.AddScoped<IRoleReadRepository, RoleReadRepository>();
        services.AddScoped<IRoleWriteRepository, RoleWriteRepository>();
        services.AddScoped<IPermissionReadRepository, PermissionReadRepository>();
        services.AddScoped<IEstateReadRepository, EstateReadRepository>();
        services.AddScoped<IEstateWriteRepository, EstateWriteRepository>();
        services.AddScoped<IEstateUserReadRepository, EstateUserReadRepository>();
        services.AddScoped<IEstateUserWriteRepository, EstateUserWriteRepository>();
        services.AddScoped<IBuildingReadRepository, BuildingReadRepository>();
        services.AddScoped<IBuildingWriteRepository, BuildingWriteRepository>();
        services.AddScoped<IBuildingTypeReadRepository, BuildingTypeReadRepository>();
        services.AddScoped<IBuildingTypeWriteRepository, BuildingTypeWriteRepository>();
        services.AddScoped<IApartmentReadRepository, ApartmentReadRepository>();
        services.AddScoped<IApartmentWriteRepository, ApartmentWriteRepository>();
        services.AddScoped<IInvitationReadRepository, InvitationReadRepository>();
        services.AddScoped<IInvitationWriteRepository, InvitationWriteRepository>();
        services.AddScoped<IVisitorBookingWriteRepository, VisitorBookingWriteRepository>();
        services.AddScoped<IVisitorBookingReadRepository, VisitorBookingReadRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
