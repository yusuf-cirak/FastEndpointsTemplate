using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Utilities.JWT;

namespace WebAPI.Data.Services.Auth
{
    public interface IAuthService
    {
        Task<AccessToken> RegisterAsync(User user,string password);
        // Register user with default role

        // Task<AccessToken> RegisterAsync(User user,string password,List<OperationClaim> operationClaims);
        // Register user with specified claims


        AccessToken LoginAsync(User user);

        
    }
}