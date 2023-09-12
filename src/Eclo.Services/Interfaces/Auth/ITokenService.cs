using Eclo.Domain.Entities.Admins;
using Eclo.Domain.Entities.Heads;
using Eclo.Domain.Entities.Users;

namespace Eclo.Services.Interfaces.Auth;

public interface ITokenService
{
    public Task<string> GenerateToken(User user);

    public Task<string> GenerateToken(Admin admin);

    public Task<string> GenerateToken(Head head);
}