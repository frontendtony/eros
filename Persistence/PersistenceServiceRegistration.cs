using Eros.Domain.Aggregates.Users;
using Eros.Persistence.Data;
using Eros.Persistence.Data.Users.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Eros.Persistence.Repositories;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ErosReadContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("Eros.Persistence")
            );
        });

        services.AddScoped<IUserReadRepository, UserReadRepository>();
        services.AddScoped<IUserWriteRepository, UserWriteRepository>();

        return services;
    }
}