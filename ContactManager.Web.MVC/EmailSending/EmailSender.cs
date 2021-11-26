using SampleAppWithDapper.Controllers;
using System;
using System.Threading.Tasks;

namespace SampleAppWithDapper.EmailSending
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string v1, string v2)
        {
            throw new NotImplementedException();
        }

        public Task SendEmailConfirmationAsync(string email, object callbackUrl)
        {
            throw new NotImplementedException();
        }
    }
}