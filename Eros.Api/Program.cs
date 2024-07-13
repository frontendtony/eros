using Eros.Api.Extensions;
using Eros.Application;
using Eros.Auth;
using Eros.Infrastructure;
using Eros.Persistence;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowAll", policy =>
  {
    policy
      .AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader();
  });
});

builder.Services.AddControllers(opt =>
  // disable automatic model state validation
  opt.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true
);

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

builder.Services.AddPersistenceServices(configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddApplicationServices();
builder.Services.RegisterAuthServices(configuration);
builder.Services.AddInfrastructureServices(configuration);

var app = builder.Build();

app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseHsts();
  app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.ConfigureExceptionHandlers();

app.Run();
