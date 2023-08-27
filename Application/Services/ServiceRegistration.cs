using System.Text;
using EstateManager.Constants;
using EstateManager.Entities;
using EstateManager.Interfaces;
using EstateManager.Handlers.CommandHandlers;
using EstateManager.Handlers.QueryHandlers;
using EstateManager.Repositories;
using EstateManager.Commands;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using EstateManager.Validators;
using EstateManager.DbContexts;

public static class ServiceExtensions
{
    public static IServiceCollection AddEstateManagerRepositories(this IServiceCollection services)
    {
        services.AddTransient<IEstateReadRepository, EstateReadRepository>();
        services.AddTransient<IEstateWriteRepository, EstateWriteRepository>();
        services.AddTransient<IUserWriteRepository, UserWriteRepository>();
        services.AddTransient<IUserReadRepository, UserReadRepository>();

        return services;
    }

    public static IServiceCollection AddEstateManagerHandlers(this IServiceCollection services)
    {
        // Estate
        services.AddScoped<GetEstateQueryHandler>();
        services.AddScoped<GetEstateBuildingsQueryHandler>();
        services.AddScoped<CreateEstateCommandHandler>();
        services.AddScoped<UpdateEstateCommandHandler>();
        services.AddScoped<DeleteEstateCommandHandler>();
        services.AddScoped<CreateEstateBuildingCommandHandler>();
        services.AddScoped<UpdateEstateBuildingCommandHandler>();

        // Authentication
        services.AddScoped<LoginCommandHandler>();
        services.AddScoped<SignupCommandHandler>();

        // User
        services.AddScoped<GetUserQueryHandler>();

        return services;
    }

    public static IServiceCollection AddValidations(this IServiceCollection services)
    {
        services.AddScoped<IValidator<UpdateEstateCommand>, UpdateEstateCommandValidator>();
        services.AddScoped<IValidator<CreateEstateCommand>, CreateEstateCommandValidator>();
        services.AddScoped<IValidator<CreateEstateBuildingCommand>, CreateEstateBuildingCommandValidator>();
        services.AddScoped<IValidator<UpdateEstateBuildingCommand>, UpdateEstateBuildingCommandValidator>();
        services.AddScoped<IValidator<LoginCommand>, LoginCommandValidator>();
        services.AddScoped<IValidator<SignupCommand>, SignupCommandValidator>();

        return services;
    }

    public static IServiceCollection AddApiDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }

    public static IServiceCollection AddIdentityService(this IServiceCollection services)
    {
        services
            .AddIdentityCore<User>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<EstateManagerDbContext>();

        return services;
    }

    public static IServiceCollection AddAuthenticationService(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = "esm.frontendtony.com", // Replace with your issuer
                    ValidAudience = "esm.frontendtony.com", // Replace with your audience
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("esm.frontendtony.com")) // Replace with your secret key
                };
            });

        return services;
    }

    public static IServiceCollection AddAuthorizationService(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireClaim(CustomClaimTypes.IsAdmin, "True"));
        });

        return services;
    }
}
