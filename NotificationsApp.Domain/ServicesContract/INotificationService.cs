using NotificationsApp.Domain.DTO.Notifictios;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationsApp.Domain.ServicesContract
{
    public interface INotificationService
    {
        public Task<IEnumerable<NotifictiosDto>> GetAllNotifictiosAsync(CancellationToken ct);

        public Task<IEnumerable<NotifictiosDto>> GetUserNotifictiosAsync(int id, CancellationToken ct);
    }
}
