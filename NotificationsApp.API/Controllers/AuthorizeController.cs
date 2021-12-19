using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotificationsApp.Domain.DTO.Authorize;
using NotificationsApp.Domain.Query;
using NotificationsApp.Domain.ServicesContract;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationsApp.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly ILogger<AuthorizeController> _logger;
        private readonly IAuthService _authService;

        /// <summary>
        /// инициализация
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="authService"></param>
        public AuthorizeController(
            ILogger<AuthorizeController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        /// <summary>
        /// authentication user on login/pass
        /// </summary>
        /// <param name="ct"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<LoginResponseDto> AuthUser
            ([FromBody] AuthorizeQuery query, CancellationToken ct = default)
        {
            var token = await _authService.AuthorizeUser(query.UserName, query.Password, ct);
            return token;
        }

        /// <summary>
        /// register user
        /// </summary>
        /// <param name="ct"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<LoginResponseDto> RegisterUser
            ([FromBody] RegisterUserQuery query, CancellationToken ct = default)
        {
            var token = await _authService.RegisterUser(query.UserName, query.Password, query.Email, ct);
            return token;
        }
    }
}
