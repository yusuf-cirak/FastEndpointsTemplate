using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Repositories;
using WebAPI.Models;
using WebAPI.Utilities.Security.Hashing;

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

        public async Task<User?> GetUserByEmail(string email,bool includeOperationClaims=false)
        {
            if (includeOperationClaims)
            {
                return await _userRepository.GetAsync(predicate:u=>u.Email==email,
                include:i=>i.Include(u=>u.UserOperationClaims)
                .ThenInclude(uop=>uop.OperationClaim),enableTracking:false);
            }

            return await _userRepository.GetAsync(predicate:e=>e.Email==email,enableTracking:false);
        }

        public async Task<User?> GetUserByUserName(string userName,bool includeOperationClaims=false)
        {
          if (includeOperationClaims)
            {
                return await _userRepository.GetAsync(predicate:u=>u.UserName==userName,
                include:i=>i.Include(u=>u.UserOperationClaims)
                .ThenInclude(uop=>uop.OperationClaim),enableTracking:false);
            }

            return await _userRepository.GetAsync(predicate:e=>e.UserName==userName,enableTracking:false);
        }
        public async Task<User> GetUserById(string id,bool includeOperationClaims=false)
        {
          if (includeOperationClaims)
            {
                return await _userRepository.GetAsync(predicate:u=>u.Id==id,
                include:i=>i.Include(u=>u.UserOperationClaims)
                .ThenInclude(uop=>uop.OperationClaim),enableTracking:false);
            }

            return await _userRepository.GetAsync(predicate:e=>e.Id==id,enableTracking:false);
        }

        public User CreateUserIdAndHashedPassword(User user, string password)
        {
            HashingHelper.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            user.Id=Guid.NewGuid().ToString();

            return user;
        }



        public async Task<List<UserOperationClaim>> GetUserOperationClaimsByUserId(string id)
        {
            var data= await _userRepository.GetAsync
           (predicate:e=>e.Id==id,
           include:e=>e.Include(e=>e.UserOperationClaims).ThenInclude(e=>e.OperationClaim),enableTracking:false);

          return data.UserOperationClaims.ToList();
        }

        public  async Task<User> AddUserOperationClaimAsync(User user,OperationClaim operationClaim)
        {
            UserOperationClaim userOperationClaim=new(Guid.NewGuid().ToString(),user.Id,operationClaim.Id);
            user.UserOperationClaims.Add(userOperationClaim);

           await _userOperationClaimRepository.AddAsync(userOperationClaim);
           
            return user;
        }

        public async Task<User> AddUserOperationClaimsAsync(User user, List<UserOperationClaim> userOperationClaims)
        {
            foreach (var userOperationClaim in userOperationClaims)
            {
                user.UserOperationClaims.Add(userOperationClaim);
            }

                await _userOperationClaimRepository.AddRangeAsync(userOperationClaims);

            return user;
        }

        public async Task<IList<User>> GetAllUsers()
        {
            return await _userRepository.GetListAsync(enableTracking:false);
        }

        public IQueryable<User> Query()
        {
            return _userRepository.Query();
        }

        public async Task DeleteUserAsync(User user)
        {
           await _userRepository.DeleteAsync(user);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            await _userRepository.UpdateAsync(user);
            return user;
        }
    }
}