using Microsoft.EntityFrameworkCore;
using Application.Extensions;
using EstateManager.DbContexts;
using Eros.Persistence.Repositories;
using Eros.Application.Services;

namespace Eros.Api;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddApiDocumentation();
        services.AddIdentityService();
        services.AddAuthenticationService();
        services.AddAuthorizationService();
        services.AddValidations();

        services.AddPersistenceServices(Configuration);
        services.AddDbContext<EstateManagerDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

        services.AddTransient<ITokenService, TokenService>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddEstateManagerRepositories();
        services.AddEstateManagerHandlers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.ConfigureExceptionHandlers();

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseHsts();
            app.UseHttpsRedirection();
        }

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

}
