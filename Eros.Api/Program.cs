using Eros.Api.Middlewares;
using Eros.Application;
using Eros.Application.Features.Users.CommandHandlers;
using Eros.Auth;
using Eros.Persistence;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme.",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
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
    }
);
builder.Services.AddControllers();
builder.Services.AddPersistenceServices(configuration);
builder.Services.RegisterAuthServices(configuration);
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(SignupCommandHandler).Assembly);
});
builder.Services.AddValidators();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
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
app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();
app.Run();
