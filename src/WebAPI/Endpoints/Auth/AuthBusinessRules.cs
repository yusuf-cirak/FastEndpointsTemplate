using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Repositories;
using WebAPI.Data.Services.Auth;
using WebAPI.Models;
using WebAPI.Utilities.Security.Hashing;

namespace WebAPI.Endpoints.Auth
{
    public sealed class AuthBusinessRules
    {
        private readonly IUserService _userService;

        public AuthBusinessRules(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<User> UserMustExistBeforeLogin(string emailOrUserName)
        {

            User? user = 
                await _userService.GetUserByEmail(emailOrUserName,includeOperationClaims:true) ??
                await _userService.GetUserByUserName(emailOrUserName,includeOperationClaims:true);

            if (user == null)
            {
                throw new Exception("User does not exist");
            }

            return user;
        }

        public void VerifyUserPassword(User user,string password)
        {

            if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception("Wrong credentials");
            }
        }
    }
}