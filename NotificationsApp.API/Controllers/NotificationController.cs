using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotificationsApp.Domain.DTO.Notifictios;
using NotificationsApp.Domain.ServicesContract;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationsApp.API.Controllers
{
    [Route("api/notification")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly ILogger<NotificationController> _logger;
        private readonly INotificationService _service;

        /// <summary>
        /// инициализация
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="service"></param>
        public NotificationController(
            ILogger<NotificationController> logger, INotificationService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IEnumerable<NotifictiosDto>> GetAllNotifictios(CancellationToken ct = default)
        {
            return await _service.GetAllNotifictiosAsync(ct);
        }

        [HttpGet("user")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IEnumerable<NotifictiosDto>> GetUserNotifictios(
            [FromQuery] int id, CancellationToken ct = default)
        {
            return await _service.GetUserNotifictiosAsync(id, ct);
        }
    }
}
