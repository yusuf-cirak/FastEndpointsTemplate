using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using WebAPI.Data.Repositories;
using WebAPI.Data.Services.Auth;
using WebAPI.Mapping;
using WebAPI.Middlewares.Validation;
using WebAPI.Models;
using WebAPI.Utilities.JWT;

namespace WebAPI.Endpoints.Auth
{
    public sealed class LoginUserRequest
    {
        public string EmailOrUserName { get; set; }
        public string Password { get; set; }
    }

    public sealed class LoginUserEndpoint : Endpoint<LoginUserRequest, LoginUserResponse,LoginUserMapper>
    {
        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("auth/login");
            AllowAnonymous();
            Summary(s =>
            {
                s.Summary = "Login user with credentials";
                s.Description = "Login with parameters below";
            });
            Validator<LoginUserRequestValidator>();
        }

            private readonly IAuthService _authService;
            private readonly AuthBusinessRules _businessRules;

        public LoginUserEndpoint(IAuthService authService, AuthBusinessRules businessRules)
        {
            _authService = authService;
            _businessRules = businessRules;
        }

        public override async Task HandleAsync(LoginUserRequest req, CancellationToken ct)
        {
            User user= await _businessRules.UserMustExistBeforeLogin(req.EmailOrUserName);

            _businessRules.VerifyUserPassword(user,req.Password);
            
            AccessToken token = _authService.LoginAsync(user);

            await SendAsync(Map.ToResponseEntity(token));
        }
    }

    public sealed class LoginUserResponse
    {
        public AccessToken AccessToken { get; set; }
    }
}