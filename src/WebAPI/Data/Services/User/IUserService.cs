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

        Task<User?> GetUserByEmail(string email,bool includeOperationClaims=false);
        Task<User?> GetUserByUserName(string userName,bool includeOperationClaims=false);
        Task<User> GetUserById(string id,bool includeOperationClaims=false);


        
        User CreateUserIdAndHashedPassword(User user, string password);


        Task<List<UserOperationClaim>> GetUserOperationClaimsByUserId(string id);
        Task<User> AddUserOperationClaimAsync(User user,OperationClaim operationClaim);
        Task<User> AddUserOperationClaimsAsync(User user, List<UserOperationClaim> userOperationClaims);


        Task<IList<User>> GetAllUsers();
        IQueryable<User> Query();
        Task DeleteUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
    }
}