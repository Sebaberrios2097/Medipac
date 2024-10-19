using Microsoft.AspNetCore.Identity.UI.Services;

namespace Medipac.ReadOnly
{
    public class FakeEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Simular el envío de correo. No hace nada.
            return Task.CompletedTask;
        }
    }
}
