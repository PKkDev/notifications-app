using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotificationsApp.Domain.Query;
using NotificationsApp.Domain.ServicesContract;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationsApp.API.Controllers
{
    [Route("api/notifictios")]
    [ApiController]
    public class AcceptNotifictioController : ControllerBase
    {
        private readonly ILogger<AcceptNotifictioController> _logger;
        private readonly IAcceptNotification _service;

        /// <summary>
        /// инициализация
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="service"></param>
        public AcceptNotifictioController(
            ILogger<AcceptNotifictioController> logger, IAcceptNotification service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// отправка уведомления
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /notifictios
        ///     {
        ///         "date": "2021-10-03T09:59:16.023Z",
        ///         "message": "test",
        ///         "system": "SystemOne",
        ///         "theme": "ThemeOne"
        ///     }
        /// </remarks>
        /// <param name="query"></param>
        /// <param name="ct"></param>
        [HttpPost]
        [AllowAnonymous]
        public async Task AcceptNotifications(
            [FromBody] SendNotifictiosQuery query, CancellationToken ct = default)
        {
            await _service.AcceptNotificationAsync(query, ct);
        }
    }
}
