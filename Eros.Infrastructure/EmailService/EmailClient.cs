using Eros.Application.EmailService;
using FluentEmail.Core;
using FluentEmail.Core.Models;

namespace Eros.Infrastructure.EmailService;

public class EmailClient(IFluentEmailFactory fluentEmail) : IEmailClient
{
    public async Task Send(string to, string body, string subject)
    {
        await fluentEmail
            .Create()
            .To(to)
            .Subject(subject)
            .Body(body, true)
            .SendAsync();
    }

    public async Task Send(List<string> tos, string body, string subject)
    {
        var emails = tos?.Select(emails => new Address(emails)).ToList();
        await fluentEmail
            .Create()
            .To(emails)
            .Subject(subject)
            .Body(body, true)
            .SendAsync();
    }
}