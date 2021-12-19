using NotificationsApp.Domain.DTO.Dictionary;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationsApp.Domain.ServicesContract
{
    public interface IDictionaryService
    {
        public Task<IEnumerable<SystemDto>> GetSystemsAsync(CancellationToken ct);
        public Task AddSystemAsync(string name, CancellationToken ct);
        public Task UpdateSystemAsync(int id, string newName, CancellationToken ct);
        public Task RemoveSystemAsync(int id, CancellationToken ct);

        public Task AddThemeAsync(int systemId, string name, CancellationToken ct);
        public Task UpdateThemeAsync(int systemId, int themeId, string newName, CancellationToken ct);
        public Task RemoveThemeAsync(int systemId, int themeId, CancellationToken ct);
    }
}
