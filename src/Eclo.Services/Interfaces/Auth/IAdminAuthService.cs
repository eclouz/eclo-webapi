using Eclo.Persistence.Dtos.Auth;

namespace Eclo.Services.Interfaces.Auth;

public interface IAdminAuthService
{
    public Task<(bool Result, string Token)> LoginAsync(LoginDto loginDto);
}
