using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Data.Services.Auth
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(User user);

        User AddUserOperationClaimAsync(User user,UserOperationClaim userOperatinClaim);
        User AddUserOperationClaimsAsync(User user,IList<UserOperationClaim> userOperationClaims);

        Task<User> GetUserByEmail(string email);



    }
}