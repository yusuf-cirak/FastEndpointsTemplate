using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Repositories;
using WebAPI.Data.Repositories.User;
using WebAPI.Helpers.Hashing;
using WebAPI.Helpers.Token;
using WebAPI.Models;

namespace WebAPI.Data.Services.Auth
{
    public sealed class AuthService : IAuthService
    {
        private readonly ITokenHandler _tokenHandler;
        private readonly IUserService _userService;


        public AuthService(IUserService userService, ITokenHandler tokenHandler)
        {
            _userService = userService;
            _tokenHandler = tokenHandler;
        }

        public async Task<AccessToken> RegisterAsync(User user, string password)
        {
            HashingHelper.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;

            user = await _userService.CreateUserAsync(user);

            return _tokenHandler.CreateAccessToken(user, user.UserOperationClaims.Select(e => e.OperationClaim).ToList());

        }
        public async Task<AccessToken> LoginAsync(string email, string password)
        {

          User user = await _userService.GetUserByEmail(email);

            if (user!=null &&!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception("User credentials wrong");
            }

            List<OperationClaim> operationClaims = new();
            foreach (var userOperationClaim in user.UserOperationClaims)
            {
                operationClaims.Add(userOperationClaim.OperationClaim);
            }

            AccessToken token = _tokenHandler.CreateAccessToken(user, operationClaims);

            return token;

        }
    }
}