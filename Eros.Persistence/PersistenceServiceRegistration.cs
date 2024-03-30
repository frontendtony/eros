using Eros.Domain.Aggregates.Apartments;
using Eros.Domain.Aggregates.Buildings;
using Eros.Domain.Aggregates.Estates;
using Eros.Domain.Aggregates.Estates.Repositories;
using Eros.Domain.Aggregates.Roles;
using Eros.Domain.Aggregates.Users;
using Eros.Persistence.Data.Apartments.Repositories;
using Eros.Persistence.Data.Buildings.Repositories;
using Eros.Persistence.Data.Estates.Repositories;
using Eros.Persistence.Data.Roles.Repositories;
using Eros.Persistence.Data.Users.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Eros.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
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
        services.AddScoped<IEstateReadRepository, EstateReadRepository>();
        services.AddScoped<IEstateWriteRepository, EstateWriteRepository>();
        services.AddScoped<IBuildingReadRepository, BuildingReadRepository>();
        services.AddScoped<IBuildingWriteRepository, BuildingWriteRepository>();
        services.AddScoped<IBuildingTypeReadRepository, BuildingTypeReadRepository>();
        services.AddScoped<IBuildingTypeWriteRepository, BuildingTypeWriteRepository>();
        services.AddScoped<IApartmentReadRepository, ApartmentReadRepository>();
        services.AddScoped<IApartmentWriteRepository, ApartmentWriteRepository>();

        return services;
    }
}