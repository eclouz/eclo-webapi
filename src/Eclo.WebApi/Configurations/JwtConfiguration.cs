using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Eclo.WebApi.Configurations;

public static class JwtConfiguration
{
    public static void ConfigureJwtAuth(this WebApplicationBuilder builder)
    {
        var config = builder.Configuration.GetSection("Jwt");

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = config["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = config["Audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["SecurityKey"]!))
                };
            });
    }
}
