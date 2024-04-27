namespace Eros.Application.EmailService;

public interface IEmailClient
{
    public Task Send(string to, string body, string subject);
    public Task Send(List<string> tos, string body, string subject);
}