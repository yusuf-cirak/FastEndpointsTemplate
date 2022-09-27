using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using WebAPI.Endpoints.Auth;
using WebAPI.Models;
using WebAPI.Utilities.JWT;

namespace WebAPI.Mapping
{
    public sealed class LoginUserMapper:Mapper<LoginUserRequest,LoginUserResponse,AccessToken>
    {
        public LoginUserResponse ToResponseEntity(AccessToken token)
        =>new(){AccessToken=token};
    }
}