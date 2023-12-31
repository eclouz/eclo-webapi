﻿using Eclo.Persistence.Dtos.Auth;

namespace Eclo.Services.Interfaces.Auth;

public interface IUserAuthService
{
    public Task<(bool Result, int CachedMinutes)> RegisterAsync(RegisterDto dto);

    public Task<(bool Result, int CachedVerificationMinutes)> SendCodeForRegisterAsync(string phone);

    public Task<(bool Result, string Token)> VerifyRegisterAsync(string phone, int code);

    public Task<(bool Result, string Token)> LoginAsync(LoginDto loginDto);

    public Task<(bool Result, int CachedMinutes)> UpdatePasswordAsync(ResetPasswordDto dto);

    public Task<(bool Result, int CachedVerificationMinutes)> SendCodeForResetPasswordAsync(string phone);

    public Task<(bool Result, string Token)> VerifyResetPasswordAsync(string phone, int code);

    public Task<bool> ResetPasswordAsync(ResetPasswordDto resetDto);
}
