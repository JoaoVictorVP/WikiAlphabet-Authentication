namespace Authentication.Server.XIdentity.Contracts.Services;

public interface IEmailService
{
    Task SendEmail(string to, string subject, string body);
}