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

        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserByUserName(string userName);
        Task<User> GetUserById(int id);



        Task<List<UserOperationClaim>> GetUserOperationClaimsByUserId(int id);
        User AddUserOperationClaimAsync(User user, UserOperationClaim userOperatinClaim);
        User AddUserOperationClaimsAsync(Models.User user, List<UserOperationClaim> userOperationClaims);

    }
}