using EfData.Context;
using EfData.Entities;
using EfData.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NotificationsApp.Domain.DTO.Subscription;
using NotificationsApp.Domain.Query;
using NotificationsApp.Domain.ServicesContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationsApp.Infrastructure.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ILogger<SubscriptionService> _logger;
        private readonly ApplicationContext _context;

        public SubscriptionService(
            ILogger<SubscriptionService> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionAsync(
            CancellationToken ct = default)
        {
            try
            {
                var result = new List<SubscriptionDto>();

                var subs = await _context.UserSubscription
                    .Include(x => x.User)
                    .Include(x => x.SystemsDictionary)
                    .Include(x => x.ThemeDictionary)
                    .ToListAsync(ct);

                foreach (var sub in subs)
                    result.Add(new SubscriptionDto()
                    {
                        Id = sub.Id,
                        System = sub.SystemsDictionary.Name,
                        Theme = sub.ThemeDictionary.Name,
                        UserName = sub.User.UserName,
                        Name = $"{sub.User.FName} {sub.User.SName}",
                        Email = sub.User.Email,
                        Phone = sub.User.Phone,
                        Type = Enum.GetName(typeof(TypeSubscription), sub.Type)
                    });

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError($"{e}");
                throw;
            }
        }

        public async Task<IEnumerable<SubscriptionDto>> GetUserSubscriptionAsync(
            int id, CancellationToken ct = default)
        {
            try
            {
                var result = new List<SubscriptionDto>();

                var subs = await _context.UserSubscription
                    .Where(x => x.UserId == id)
                    .Include(x => x.SystemsDictionary)
                    .Include(x => x.ThemeDictionary)
                    .Include(x => x.User)
                    .ToListAsync(ct);

                foreach (var sub in subs)
                    result.Add(new SubscriptionDto()
                    {
                        Id = sub.Id,
                        System = sub.SystemsDictionary.Name,
                        Theme = sub.ThemeDictionary.Name,
                        UserName = sub.User.UserName,
                        Name = $"{sub.User.FName} {sub.User.SName}",
                        Email = sub.User.Email,
                        Phone = sub.User.Phone,
                        Type = Enum.GetName(typeof(TypeSubscription), sub.Type)
                    });

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError($"{e}");
                throw;
            }
        }

        public async Task AddUserSubscriptionAsync(
            int id, AddSubscriptionQuery dto, CancellationToken ct = default)
        {
            try
            {
                //id = 7;
                //dto.ThemeId = 14;
                //dto.SystemId = 6;
                //dto.Type = TypeSubscription.Mail;

                //var users = await _context.User.ToListAsync(ct);

                //var themes = await _context.SystemsDictionary
                //     .Include(x => x.Themes)
                //     .ToListAsync(ct);

                UserSubscription newUserSubscription = new UserSubscription()
                {
                    SystemId = dto.SystemId,
                    ThemeId = dto.ThemeId,
                    UserId = id,
                    Type = dto.Type
                };

                await _context.UserSubscription.AddAsync(newUserSubscription, ct);
                await _context.SaveChangesAsync(ct);
            }
            catch (Exception e)
            {
                _logger.LogError($"{e}");
                throw;
            }
        }

        public async Task DeleteUserSubscriptionAsync(int id, CancellationToken ct = default)
        {
            try
            {
                var subs = await _context.UserSubscription
                    .FirstOrDefaultAsync(x => x.Id == id, ct);

                if (subs == null)
                    throw new Exception("подписка не анйдена");

                _context.UserSubscription.Remove(subs);
                await _context.SaveChangesAsync(ct);
            }
            catch (Exception e)
            {
                _logger.LogError($"{e}");
                throw;
            }
        }
    }
}
