using EfData.Model;

namespace NotificationsApp.Domain.Query
{
    public class AddSubscriptionQuery
    {
        public string System { get; set; }

        public int SystemId { get; set; }

        public string Theme { get; set; }

        public int ThemeId { get; set; }

        public TypeSubscription Type { get; set; }
    }
}
