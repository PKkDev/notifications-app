using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotificationsApp.Domain.DTO.Subscription;
using NotificationsApp.Domain.Query;
using NotificationsApp.Domain.ServicesContract;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationsApp.API.Controllers
{
    [Route("api/subscription")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ILogger<SubscriptionController> _logger;
        private readonly ISubscriptionService _service;

        /// <summary>
        /// инициализация
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="service"></param>
        public SubscriptionController(
            ILogger<SubscriptionController> logger, ISubscriptionService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IEnumerable<SubscriptionDto>> GetAllSubscription(
            CancellationToken ct = default)
        {
            return await _service.GetAllSubscriptionAsync(ct);
        }

        [HttpGet("user")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IEnumerable<SubscriptionDto>> GetUserSubscription(
            [FromQuery] int id, CancellationToken ct = default)
        {
            return await _service.GetUserSubscriptionAsync(id, ct);
        }

        [HttpPost("add")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddUserSubscription(
            [FromQuery] int id, [FromBody] AddSubscriptionQuery query, CancellationToken ct = default)
        {
            await _service.AddUserSubscriptionAsync(id, query, ct);
            return Ok();
        }

        [HttpGet("rm")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteUserSubscription(
          [FromQuery] int id, CancellationToken ct = default)
        {
            await _service.DeleteUserSubscriptionAsync(id, ct);
            return Ok();
        }

    }
}
