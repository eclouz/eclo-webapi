using Eclo.Domain.Entities.Users;

namespace Eclo.Services.Interfaces.Auth;

public interface ITokenService
{
    public Task<string> GenerateToken(User user);
}