using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using WebAPI.Data.Repositories;
using WebAPI.Data.Services.Auth;
using WebAPI.Helpers.Token;
using WebAPI.Middlewares.Validation;
using WebAPI.Models;

namespace WebAPI.Endpoints.Auth
{
    public sealed class LoginUserRequest
    {
        public string EmailOrUserName { get; set; }
        public string Password { get; set; }
    }

    public sealed class LoginUserEndpoint : Endpoint<LoginUserRequest, LoginUserResponse>
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
                s.Responses[200] = "Login is successful";
                s.Responses[404] = "Wrong user credentials";

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

            
            AccessToken token = _authService.LoginAsync(user,req.Password);

            LoginUserResponse response = new(){AccessToken=token};

            await SendAsync(response);
        }
    }

    public class LoginUserResponse
    {
        public AccessToken AccessToken { get; set; }
    }
}