using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Repositories;
using WebAPI.Models;

namespace WebAPI.Data.Services.Auth
{
    public sealed class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;

        public UserService(IUserRepository repository, IUserOperationClaimRepository userOperationClaimRepository)
        {
            _userRepository = repository;
            _userOperationClaimRepository = userOperationClaimRepository;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            return await _userRepository.AddAsync(user);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
          return await _userRepository.GetAsync
          (u=>u.Email==email,
          include:u=>u.Include(u=>u.UserOperationClaims)
          .ThenInclude(uop=>uop.OperationClaim),enableTracking:false);
        }

        public async Task<User?> GetUserByUserName(string userName)
        {
          return await _userRepository.GetAsync
          (u=>u.UserName==userName,
          include:u=>u.Include(u=>u.UserOperationClaims)
          .ThenInclude(uop=>uop.OperationClaim),enableTracking:false);
        }
        public async Task<User> GetUserById(int id)
        {
          return await _userRepository.GetAsync
          (u=>u.Id==id,
          include:u=>u.Include(u=>u.UserOperationClaims)
          .ThenInclude(uop=>uop.OperationClaim),enableTracking:false);
        }

        public async Task<List<UserOperationClaim>> GetUserOperationClaimsByUserId(int id)
        {
            var data= await _userRepository.GetAsync
           (predicate:e=>e.Id==id,
           include:e=>e.Include(e=>e.UserOperationClaims).ThenInclude(e=>e.OperationClaim),enableTracking:false);

          return data.UserOperationClaims.ToList();
        }

        public  User AddUserOperationClaimAsync(User user, UserOperationClaim userOperatinClaim)
        {
            user.UserOperationClaims.Append(userOperatinClaim);
            return user;
        }

        public User AddUserOperationClaimsAsync(Models.User user, List<UserOperationClaim> userOperationClaims)
        {
            foreach (var userOperationClaim in userOperationClaims)
            {
                user.UserOperationClaims.Append(userOperationClaim);
            }

                _userOperationClaimRepository.AddRangeAsync(userOperationClaims);

            return user;
        }
    }
}