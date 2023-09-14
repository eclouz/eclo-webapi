using Eclo.Persistence.Dtos.Auth;

namespace Eclo.Services.Interfaces.Auth;

public interface IAdminAuthService
{
    public Task<(bool Result, string Token)> LoginAsync(LoginDto loginDto);

    public Task<(bool Result, int CachedVerificationMinutes)> SendCodeForResetPasswordAsync(string phone);

    public Task<(bool Result, string Token)> VerifyResetPasswordAsync(string phone, int code);
}
