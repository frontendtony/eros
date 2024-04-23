using System.Reflection;
using Eros.Application.Behaviours;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Eros.Infrastructure;

public static class DependencyInjection
{

    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var client = new SmtpClient
        {
            Host = configuration["FluentEmail:Host"],
            Port = int.Parse(configuration["FluentEmail:Port"])
        };

        var username = configuration["FluentEmail:Username"];
        var password = configuration["FluentEmail:Password"];
        if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
        {
            client.Credentials = new NetworkCredential(username, password);
        }

        services
            .AddFluentEmail(configuration["FluentEmail:SentFrom"])
            .AddSmtpSender(client);
    }
}