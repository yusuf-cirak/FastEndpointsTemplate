using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Utilities.JWT;
using WebAPI.Utilities.Security.Encryption;

namespace WebAPI.Extensions
{
    public static class JwtBearerRegistration
    {
        internal static IServiceCollection AddJwtBearerServices(this IServiceCollection services,IConfiguration configuration)
        {
            TokenOptions tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => opt.TokenValidationParameters = new()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = tokenOptions.Audience,
                ValidIssuer = tokenOptions.Issuer,
                IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
            });

            return services;
        }
    }
}