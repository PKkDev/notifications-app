using EfData.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NotificationsApp.Domain.DTO.User;
using NotificationsApp.Domain.ServicesContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationsApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly ApplicationContext _context;

        public UserService(
            ILogger<UserService> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IEnumerable<UserDto>> GetAllUserAsync(
            CancellationToken ct)
        {
            try
            {
                var users = await _context.User
                    .Select(x => new UserDto()
                    {
                        UserName = x.UserName,
                        FName = x.FName,
                        SName = x.SName,
                        Email = x.Email,
                        Phone = x.Phone,
                    })
                    .ToListAsync(ct);

                return users;
            }
            catch (Exception e)
            {
                _logger.LogError($"{e}");
                throw;
            }
        }
    }
}
