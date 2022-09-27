using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Services.Auth;
using WebAPI.Models;
using WebAPI.Utilities.Security.Extensions;

namespace WebAPI.Endpoints
{
    public sealed class UserBusinessRules
    {
        private readonly IUserService _userService;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserBusinessRules(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User> UserShouldExistBeforeDeleted(string id)
        {
            User user = await _userService.GetUserById(id, includeOperationClaims: false);

            if (user == null) throw new Exception("User does not exist");

            return user;
        }

        public void CheckUserCredentials(string id){
           string idFromToken= _httpContextAccessor.HttpContext.User.GetUserId();

           bool isUserAdmin=_httpContextAccessor.HttpContext.User.HasClaim(e=>e.Value=="Admin");

            if (idFromToken!=id || !isUserAdmin) throw new Exception("You are not authorized");

        }
    }
}