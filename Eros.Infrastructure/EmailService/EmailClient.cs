using Eros.Application.EmailService;
using FluentEmail.Core;
using FluentEmail.Core.Models;

namespace Eros.Infrastructure.EmailService;

public class EmailClient(IFluentEmail fluentEmail) : IEmailClient
{
    public async Task Send(string from, string to, string body, string subject)
    {
        await fluentEmail
            .To(to)
            .Subject(subject)
            .Body(body, true)
            .SendAsync();
    }
    
    public async Task Send(string from, List<string> tos, string body, string subject)
    {
        var emails = tos?.Select(emails => new Address(emails, null)).ToList();
        await fluentEmail
            .To(emails)
            .Subject(subject)
            .Body(body, true)
            .SendAsync();
    }
}