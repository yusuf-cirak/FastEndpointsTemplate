using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Repositories.User;
using WebAPI.Models;

namespace WebAPI.Data.Services.Auth
{
    public sealed class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository repository)
        {
            _userRepository = repository;
        }

        public async Task<Models.User> CreateUserAsync(Models.User user)
        {
            user=await _userRepository.AddAsync(user);

            List<UserOperationClaim> userOperationClaims=new();

            userOperationClaims.Add(new(0,user.Id,1));

            user= AddUserOperationClaimsAsync(user,userOperationClaims);

           await _userRepository.SaveChangesAsync();

            return user;
        }

        public async Task<Models.User> GetUserByEmail(string email)
        {
          return await _userRepository.Table.AsNoTracking()
          .Include(e=>e.UserOperationClaims)
          .ThenInclude(e=>e.OperationClaim)
          .FirstOrDefaultAsync(e=>e.Email==email);
        }


        public  User AddUserOperationClaimAsync(Models.User user, UserOperationClaim userOperatinClaim)
        {
            user.UserOperationClaims.Append(userOperatinClaim);
            return user;
        }

        public User AddUserOperationClaimsAsync(Models.User user, IList<UserOperationClaim> userOperationClaims)
        {
            foreach (var userOperationClaim in userOperationClaims)
            {
                user.UserOperationClaims.Append(userOperationClaim);
            }

            return user;
        }
    }
}