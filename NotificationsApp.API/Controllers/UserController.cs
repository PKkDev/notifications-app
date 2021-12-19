using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotificationsApp.Domain.DTO.User;
using NotificationsApp.Domain.ServicesContract;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationsApp.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _service;

        /// <summary>
        /// инициализация
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="service"></param>
        public UserController(
            ILogger<UserController> logger, IUserService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IEnumerable<UserDto>> GetAllUser(CancellationToken ct = default)
        {
            return await _service.GetAllUserAsync(ct);
        }
    }
}
