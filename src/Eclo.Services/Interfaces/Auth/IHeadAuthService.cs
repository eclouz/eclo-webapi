using Eclo.Persistence.Dtos.Auth;

namespace Eclo.Services.Interfaces.Auth;

public interface IHeadAuthService
{
    public Task<(bool Result, string Token)> LoginAsync(LoginDto loginDto);
}
