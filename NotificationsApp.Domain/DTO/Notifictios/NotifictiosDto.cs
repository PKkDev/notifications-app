using System;

namespace NotificationsApp.Domain.DTO.Notifictios
{
    public class NotifictiosDto
    {
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public string System { get; set; }
        public string Theme { get; set; }
        public bool IsSended { get; set; }
    }
}
