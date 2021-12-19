using NotificationsApp.Domain.DTO.User;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationsApp.Domain.ServicesContract
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDto>> GetAllUserAsync(CancellationToken ct);
    }
}


// client.Authenticate("prodevkir@mail.ru", "ZlZlZlJeME2");
// email.From.Add(new MailboxAddress("System Notif", "prodevkir@mail.ru"));