namespace Eros.Application.EmailService;

public interface IEmailClient
{
    public Task Send(string from, string to, string body, string subject);
    public Task Send(string from, List<string> tos, string body, string subject);
}