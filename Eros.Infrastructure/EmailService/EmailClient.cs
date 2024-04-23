using Eros.Application.EmailService;
using FluentEmail.Core;
using FluentEmail.Core.Models;

namespace Eros.Infrastructure.EmailService;

public class EmailClient : IEmailClient
{
    private readonly IFluentEmail _fluentEmail;

    public EmailClient(IFluentEmail fluentEmail)
    {
        _fluentEmail = fluentEmail;
    }

    public async Task Send(string from, string to, string body, string subject)
    {
        var response = await _fluentEmail
                   .To(to)
                   .Subject(subject)
                   .Body(body, true)
                   .SendAsync();

        return;
    }
    public async Task Send(string from, List<string> tos, string body, string subject)
    {
        var emails = tos?.Select(emails => new Address(emails, null)).ToList();
        var response = await _fluentEmail
                    .To(emails)
                    .Subject(subject)
                    .Body(body, true)
                    .SendAsync();

        return;
    }
}