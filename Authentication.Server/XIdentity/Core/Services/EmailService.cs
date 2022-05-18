using Authentication.Server.XIdentity.Contracts.Services;

namespace Authentication.Server.XIdentity.Core.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string to, string subject, string body)
        {
            // Simulate sending emails...
        }
    }
}