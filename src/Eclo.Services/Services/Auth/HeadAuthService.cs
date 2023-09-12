using Eclo.Application.Exceptions.Auth;
using Eclo.Application.Exceptions.Heads;
using Eclo.DataAccess.Interfaces.Heads;
using Eclo.Persistence.Dtos.Auth;
using Eclo.Services.Interfaces.Auth;
using Eclo.Services.Security;

namespace Eclo.Services.Services.Auth;

public class HeadAuthService : IHeadAuthService
{
    private readonly IHeadRepository _headRepository;
    private readonly ITokenService _tokenService;

    public HeadAuthService(IHeadRepository headRepository,
        ITokenService tokenService)
    {
        this._headRepository = headRepository;
        this._tokenService = tokenService;
    }

    public async Task<(bool Result, string Token)> LoginAsync(LoginDto loginDto)
    {
        var head = await _headRepository.GetByPhoneAsync(loginDto.PhoneNumber);
        if (head == null) throw new HeadNotFoundException();

        var hasherResult = PasswordHasher.Verify(loginDto.Password, head.PasswordHash, head.Salt);
        if (hasherResult == false) throw new PasswordNotMatchException();

        string token = await _tokenService.GenerateToken(head);
        return (Result: true, Token: token);
    }
}
