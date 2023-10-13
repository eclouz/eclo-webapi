using Eclo.Domain.Entities.Admins;
using Eclo.Domain.Entities.Heads;
using Eclo.Domain.Entities.Users;
using Eclo.Domain.Enums;
using Eclo.Persistence.Helpers;
using Eclo.Services.Interfaces.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Eclo.Services.Services.Auth;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;

    public TokenService(IConfiguration configuration)
    {
        _config = configuration.GetSection("JWT");
    }

    public async Task<string> GenerateToken(User user)
    {

        var identityClaims = new Claim[]
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("FirstName", user.FirstName),
            new Claim("LastName", user.LastName),
            new Claim("PhoneNumber", user.PhoneNumber),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
            new Claim(ClaimTypes.Role, IdentityRole.User.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecurityKey"]!));
        var keyCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        int expiresHours = int.Parse(_config["Lifetime"]!);
        var token = new JwtSecurityToken(
            issuer: _config["Issuer"],
            audience: _config["Audience"],
            claims: identityClaims,
            expires: TimeHelper.GetDateTime().AddHours(expiresHours),
            signingCredentials: keyCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    public async Task<string> GenerateToken(Admin admin)
    {
        var identityClaims = new Claim[]
        {
            new Claim("Id", admin.Id.ToString()),
            new Claim("FirstName", admin.FirstName),
            new Claim("LastName", admin.LastName),
            new Claim("PhoneNumber", admin.PhoneNumber),
            new Claim(ClaimTypes.MobilePhone, admin.PhoneNumber),
            new Claim(ClaimTypes.Role, IdentityRole.Admin.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecurityKey"]!));
        var keyCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        int expiresHours = int.Parse(_config["Lifetime"]!);
        var token = new JwtSecurityToken(
            issuer: _config["Issuer"],
            audience: _config["Audience"],
            claims: identityClaims,
            expires: TimeHelper.GetDateTime().AddHours(expiresHours),
            signingCredentials: keyCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);

    }

    public async Task<string> GenerateToken(Head head)
    {
        var identityClaims = new Claim[]
        {
            new Claim("Id", head.Id.ToString()),
            new Claim("FirstName", head.LastName),
            new Claim("PhoneNumber", head.PhoneNumber),
            new Claim(ClaimTypes.MobilePhone, head.PhoneNumber),
            new Claim(ClaimTypes.Role, IdentityRole.Head.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecurityKey"]!));
        var keyCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        int expiresHours = int.Parse(_config["Lifetime"]!);
        var token = new JwtSecurityToken(
            issuer: _config["Issuer"],
            audience: _config["Audience"],
            claims: identityClaims,
            expires: TimeHelper.GetDateTime().AddHours(expiresHours),
            signingCredentials: keyCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
