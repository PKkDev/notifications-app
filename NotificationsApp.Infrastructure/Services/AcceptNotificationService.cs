using EfData.Context;
using EfData.Entities;
using EfData.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NotificationsApp.Domain.Query;
using NotificationsApp.Domain.ServicesContract;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationsApp.Infrastructure.Services
{
    public class AcceptNotificationService : IAcceptNotification
    {
        private readonly IMailSenderService _service;
        private readonly ILogger<AcceptNotificationService> _logger;
        private readonly ApplicationContext _context;

        public AcceptNotificationService(
            IMailSenderService service, ILogger<AcceptNotificationService> logger,
            ApplicationContext context)
        {
            _service = service;
            _logger = logger;
            _context = context;
        }

        public async Task AcceptNotificationAsync(SendNotifictiosQuery query, CancellationToken ct)
        {
            try
            {
                var systemId = await _context.SystemsDictionary.FirstOrDefaultAsync(x => x.Name == query.System);
                var themeId = await _context.ThemeDictionary.FirstOrDefaultAsync(x => x.Name == query.Theme);

                var users = await _context.UserSubscription
                    .Include(x => x.User)
                    .Where(x => x.SystemId == systemId.Id && x.ThemeId == themeId.Id &&
                    x.Type == TypeSubscription.Mail)
                    .Select(x => new
                    {
                        Id = x.User.Id,
                        Email = x.User.Email
                    })
                    .ToListAsync(ct);

                var subject = $"{query.System} - {query.Theme}";

                foreach (var mailTo in users)
                {
                    await _service.SendMailMessage(subject, query.Message, mailTo.Email);

                    var newNotification = new Notification()
                    {
                        UserId = mailTo.Id,
                        Date = query.Date,
                        Message = query.Message,
                        System = query.System,
                        Theme = query.Theme,
                        IsSended = true
                    };
                    await _context.Notification.AddAsync(newNotification);
                    await _context.SaveChangesAsync(ct);
                }

            }
            catch (Exception e)
            {
                _logger.LogError($"{e}");
                throw;
            }
        }
    }
}
