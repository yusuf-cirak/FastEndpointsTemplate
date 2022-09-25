using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Models;

namespace WebAPI.Utilities.JWT
{
    public interface ITokenHandler
    {
        AccessToken CreateAccessToken(User user, List<OperationClaim> operationClaims);
    }
}