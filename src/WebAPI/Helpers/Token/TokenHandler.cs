using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FastEndpoints.Security;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Models;

namespace WebAPI.Helpers.Token
{
    public sealed class TokenHandler:ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AccessToken CreateAccessToken(User user, List<OperationClaim> operationClaims)
        {
            return CreateJwtSecurityToken(user, operationClaims);

        }

        private AccessToken CreateJwtSecurityToken(User user, List<OperationClaim> operationClaims)
        {
            TokenOptions tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptions>();

            DateTime expiration = DateTime.Now.AddMinutes(tokenOptions.AccessTokenExpiration);

            JwtSecurityToken jwt = new(
            tokenOptions.Issuer,
            tokenOptions.Audience,
            expires: expiration,
            notBefore: DateTime.Now,
            claims: SetClaims(user, operationClaims),
            signingCredentials: SigningCredentialsHelper(SecurityKeyHelper(tokenOptions.SecurityKey))
        );

        JwtSecurityTokenHandler handler=new();

        return new AccessToken(){Token=handler.WriteToken(jwt),TokenExpiration=expiration};

        }


        private SigningCredentials SigningCredentialsHelper(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }

        private SymmetricSecurityKey SecurityKeyHelper(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }

        private IEnumerable<Claim> SetClaims(User user, IList<OperationClaim> operationClaims)
        {
            List<Claim> claims = new();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            claims.Add(new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"));

            operationClaims.ToList().ForEach(e => claims.Add(new(ClaimTypes.Role, e.Name)));

            return claims;
        }
    }
}