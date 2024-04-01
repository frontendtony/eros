using Eros.Application.Features.Auth.Commands;
using Eros.Application.Features.Auth.Validators;
using Eros.Application.Features.Estates.Commands;
using Eros.Application.Features.Estates.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Eros.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<UpdateEstateCommand>, UpdateEstateCommandValidator>();
        services.AddScoped<IValidator<CreateEstateCommand>, CreateEstateCommandValidator>();
        services.AddScoped<IValidator<CreateEstateBuildingCommand>, CreateEstateBuildingCommandValidator>();
        services.AddScoped<IValidator<UpdateEstateBuildingCommand>, UpdateEstateBuildingCommandValidator>();
        services.AddScoped<IValidator<LoginCommand>, LoginCommandValidator>();
        services.AddScoped<IValidator<SignupCommand>, SignupCommandValidator>();

        return services;
    }
}
