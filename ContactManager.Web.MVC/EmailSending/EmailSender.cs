using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using SampleAppWithDapper.Controllers;
using MailKit.Net.Smtp;


namespace SampleAppWithDapper.EmailSending
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpOptions _appSettings;

        public EmailSender(SmtpOptions appSettings)
        {
            _appSettings = appSettings;
        }

        /// <summary>
        ///     Send an email via gMail, the settings (username, password, server, port) are read from the appsettings.json file
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="html"></param>
        /// <param name="from"></param>
        public string Send(string to, string subject, string html, string from = null)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from ?? _appSettings.EmailIsFrom));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(_appSettings.Host, _appSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_appSettings.UserName, _appSettings.Password);
            smtp.Send(email);
            smtp.Disconnect(true);

            return "OK";
        }

    }
}