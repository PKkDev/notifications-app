using EfData.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NotificationsApp.Domain.DTO.Notifictios;
using NotificationsApp.Domain.ServicesContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationsApp.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ILogger<NotificationService> _logger;
        private readonly ApplicationContext _context;

        public NotificationService(
            ILogger<NotificationService> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IEnumerable<NotifictiosDto>> GetAllNotifictiosAsync(CancellationToken ct)
        {
            try
            {
                var result = await _context.Notification
                    .Select(x => new NotifictiosDto
                    {
                        Date = x.Date,
                        IsSended = x.IsSended,
                        Message = x.Message,
                        System = x.System,
                        Theme = x.Theme
                    })
                    .ToListAsync(ct);

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError($"{e}");
                throw;
            }
        }

        public async Task<IEnumerable<NotifictiosDto>> GetUserNotifictiosAsync(
            int id, CancellationToken ct)
        {
            try
            {
                var result = await _context.Notification
                    .Where(x => x.UserId == id)
                    .Select(x => new NotifictiosDto
                    {
                        Date = x.Date,
                        IsSended = x.IsSended,
                        Message = x.Message,
                        System = x.System,
                        Theme = x.Theme
                    })
                    .ToListAsync(ct);

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError($"{e}");
                throw;
            }
        }
    }
}
