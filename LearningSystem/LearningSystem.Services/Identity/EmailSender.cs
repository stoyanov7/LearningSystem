namespace LearningSystem.Services.Identity
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.Extensions.Configuration;
    using SendGrid;
    using SendGrid.Helpers.Mail;

    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initialize a new instance of the <see cref="EmailSender"/> class.
        /// </summary>
        /// <param name="configuration"></param>
        public EmailSender(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Method for sending a validation e-mail.
        /// </summary>
        /// <param name="email">Email for validation.</param>
        /// <param name="subject">Subject of the message.</param>
        /// <param name="htmlMessage">HTML message.</param>
        /// <returns>Awaitable task is returned.</returns>
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var apiKey = this.configuration["SendGrid:ApiKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(this.configuration["Admin:Email"], "Learning System Admin");
            var to = new EmailAddress(email, email);
            var message = MailHelper.CreateSingleEmail(from, to, subject, htmlMessage, htmlMessage);
            var response = await client.SendEmailAsync(message);
        }
    }
}