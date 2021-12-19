using System;

namespace NotificationsApp.Domain.Query
{
    public class SendNotifictiosQuery
    {
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public string System { get; set; }
        public string Theme { get; set; }
    }
}
