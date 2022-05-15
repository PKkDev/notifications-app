using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotificationsApp.Domain.Query;
using NotificationsApp.Domain.ServicesContract;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationsApp.API.Controllers
{
    [Route("api/notification")]
    [ApiController]
    public class AcceptNotificationController : ControllerBase
    {
        private readonly ILogger<AcceptNotificationController> _logger;
        private readonly IAcceptNotification _service;

        /// <summary>
        /// инициализация
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="service"></param>
        public AcceptNotificationController(
            ILogger<AcceptNotificationController> logger, IAcceptNotification service)
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
        ///     POST /notification
        ///     {
        ///         "date": "2022-04-04T17:50:31.108Z",
        ///         "message": "Error",
        ///         "system": "IBM Notes",
        ///         "theme": "Lotus"
        ///     }
        /// </remarks>
        /// <param name="query"></param>
        /// <param name="ct"></param>
        [HttpPost]
        [AllowAnonymous]
        public async Task AcceptNotification(
            [FromBody] SendNotifictiosQuery query, CancellationToken ct = default)
        {
            await _service.AcceptNotificationAsync(query, ct);
        }
    }
}
