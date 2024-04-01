using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Eros.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
