using Authentication.Server.XIdentity.Contracts.Services;

namespace Authentication.Server.XIdentity.Core.Services
{
    public class EmailService : IEmailService
    {
        public Task SendEmail(string to, string subject, string body)
        {
            // Simulate sending emails...
            return Task.CompletedTask;
        }
    }
}