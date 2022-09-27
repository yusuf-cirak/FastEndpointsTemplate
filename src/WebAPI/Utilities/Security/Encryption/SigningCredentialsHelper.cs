using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Helpers.Security.Encryption
{
    public sealed class SigningCredentialsHelper
    {
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        { 
            return new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256); 

        }
    }
}