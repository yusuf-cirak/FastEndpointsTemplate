using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Repositories;
using WebAPI.Endpoints.Auth;
using WebAPI.Models;
using WebAPI.Utilities.JWT;

namespace WebAPI.Data.Services.Auth
{
    public sealed class AuthService : IAuthService
    {
        private readonly ITokenHandler _tokenHandler;
        private readonly IUserService _userService;
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;

        private readonly IOperationClaimRepository _operationClaimRepository;


        private readonly AuthBusinessRules _businessRules;


        public AuthService(ITokenHandler tokenHandler, IUserService userService, IOperationClaimRepository operationClaimRepository, IUserOperationClaimRepository userOperationClaimRepository, AuthBusinessRules businessRules)
        {
            _tokenHandler = tokenHandler;
            _userService = userService;
            _operationClaimRepository = operationClaimRepository;
            _userOperationClaimRepository = userOperationClaimRepository;
            _businessRules = businessRules;
        }

        public async Task<AccessToken> RegisterAsync(User user, string password)
        {
            user = _userService.CreateUserIdAndHashedPassword(user, password);

            List<OperationClaim> operationClaim = await CreateUserWithDefaultOperationClaim(user);

            return _tokenHandler.CreateAccessToken(user, operationClaim);

        }
        public AccessToken LoginAsync(User user)
        {
            List<OperationClaim> operationClaims = user.UserOperationClaims.Select(e => e.OperationClaim).ToList();

            AccessToken token = _tokenHandler.CreateAccessToken(user, operationClaims);

            return token;

        }





        private async Task<List<OperationClaim>> CreateUserWithDefaultOperationClaim(User user)
        {

            List<OperationClaim> operationClaims = new();
            operationClaims.Add(new("7fb00e19-8029-4ded-81d4-f8594b584490", "User")); // Seed data

            await _userService.CreateUserAsync(user);


            user = await _userService.AddUserOperationClaimAsync(user, operationClaims.First());

            return operationClaims;
        }
    }
}