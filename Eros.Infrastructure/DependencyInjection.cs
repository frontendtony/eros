using System.Net;
using System.Net.Mail;
using Eros.Application.EmailService;
using Eros.Infrastructure.EmailService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eros.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var client = new SmtpClient
        {
            Host = configuration["EmailConfiguration:Host"] ??
                   throw new InvalidOperationException("Email host is required."),
            Port = int.Parse(configuration["EmailConfiguration:Port"] ??
                             throw new InvalidOperationException("Email port is required.")),
            EnableSsl = true
        };

        var username = configuration["EmailConfiguration:Username"];
        var password = configuration["EmailConfiguration:Password"];

        if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            client.Credentials = new NetworkCredential(username, password);

        services
            .AddFluentEmail(configuration["EmailConfiguration:From"])
            .AddSmtpSender(client);

        services.AddScoped<IEmailClient, EmailClient>();
    }
}