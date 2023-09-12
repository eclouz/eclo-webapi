using Eclo.Application.Exceptions.Admins;
using Eclo.Application.Exceptions.Auth;
using Eclo.DataAccess.Interfaces.Admins;
using Eclo.Persistence.Dtos.Auth;
using Eclo.Services.Interfaces.Auth;
using Eclo.Services.Security;

namespace Eclo.Services.Services.Auth;

public class AdminAuthService : IAdminAuthService
{
    private readonly IAdminRepository _adminRepository;
    private readonly ITokenService _tokenService;

    public AdminAuthService(IAdminRepository adminRepository,
        ITokenService tokenService)
    {
        this._adminRepository = adminRepository;
        this._tokenService = tokenService;
    }
    public async Task<(bool Result, string Token)> LoginAsync(LoginDto loginDto)
    {
        var admin = await _adminRepository.GetByPhoneNumberAsync(loginDto.PhoneNumber);
        if (admin == null) throw new AdminNotFoundException();

        var hasherResult = PasswordHasher.Verify(loginDto.Password, admin.PasswordHash, admin.Salt);
        if (hasherResult == false) throw new PasswordNotMatchException();

        string token = await _tokenService.GenerateToken(admin);
        return (Result: true, Token: token);
    }
}
