using System.Threading.Tasks;

namespace NotificationsApp.Domain.ServicesContract
{
    public interface IMailSenderService
    {
        public Task SendMailMessage(string subject, string message, string mailTo);
    }
}
