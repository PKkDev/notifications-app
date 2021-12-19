using NotificationsApp.Domain.DTO.Subscription;
using NotificationsApp.Domain.Query;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationsApp.Domain.ServicesContract
{
    public interface ISubscriptionService
    {
        public Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionAsync(
            CancellationToken ct = default);

        public Task<IEnumerable<SubscriptionDto>> GetUserSubscriptionAsync(
           int id, CancellationToken ct = default);
        public Task AddUserSubscriptionAsync(
           int id, AddSubscriptionQuery dto, CancellationToken ct = default);
        public Task DeleteUserSubscriptionAsync(
          int id, CancellationToken ct = default);
    }
}
