using NotificationsApp.Domain.Query;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationsApp.Domain.ServicesContract
{
    public interface IAcceptNotification
    {
        public Task AcceptNotificationAsync(SendNotifictiosQuery query, CancellationToken ct);
    }
}
