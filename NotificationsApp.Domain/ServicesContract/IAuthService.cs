using NotificationsApp.Domain.DTO.Authorize;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationsApp.Domain.ServicesContract
{
    public interface IAuthService
    {
        public Task<LoginResponseDto> AuthorizeUser(string userName, string password, CancellationToken ct);

        public Task<LoginResponseDto> RegisterUser(string userName, string password, string email, CancellationToken ct);
    }
}
