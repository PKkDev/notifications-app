using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotificationsApp.Domain.DTO.Dictionary;
using NotificationsApp.Domain.ServicesContract;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationsApp.API.Controllers
{
    [Route("api/dictionary")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        private readonly ILogger<DictionaryController> _logger;
        private readonly IDictionaryService _service;

        /// <summary>
        /// инициализация
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="service"></param>
        public DictionaryController(
            ILogger<DictionaryController> logger, IDictionaryService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IEnumerable<SystemDto>> GetSystems(
           CancellationToken ct = default)
        {
            return await _service.GetSystemsAsync(ct);
        }

        [HttpGet("sys-add")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task AddSystem(
            [FromQuery] string name, CancellationToken ct = default)
        {
            await _service.AddSystemAsync(name, ct);
        }
        [HttpGet("sys-up")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task UpdateSystem(
           [FromQuery] string name, [FromQuery] int id, CancellationToken ct = default)
        {
            await _service.UpdateSystemAsync(id, name, ct);
        }
        [HttpGet("sys-rm")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task RemoveSystem(
           [FromQuery] int id, CancellationToken ct = default)
        {
            await _service.RemoveSystemAsync(id, ct);
        }



        [HttpGet("them-add")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task AddTheme(
          [FromQuery] int systemId, [FromQuery] string name, CancellationToken ct = default)
        {
            await _service.AddThemeAsync(systemId, name, ct);
        }
        [HttpGet("them-up")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task UpdateTheme(
            [FromQuery] int systemId, [FromQuery] int themeId, [FromQuery] string name, CancellationToken ct = default)
        {
            await _service.UpdateThemeAsync(systemId, themeId, name, ct);
        }
        [HttpGet("them-rm")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task RemoveTheme(
            [FromQuery] int systemId, [FromQuery] int themeId, CancellationToken ct = default)
        {
            await _service.RemoveThemeAsync(systemId, themeId, ct);

        }
    }
}
