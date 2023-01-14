using System.Threading.Tasks;

namespace SampleAppWithDapper.Controllers
{
    public interface IEmailSender
    {
        //Task SendEmailConfirmationAsync(string email, object callbackUrl);

        //Task SendEmailAsync(string email, string v1, string v2);

        string Send(string to, string subject, string html, string from = null);
    }
}