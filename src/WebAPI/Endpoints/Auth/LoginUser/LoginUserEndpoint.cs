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
        public string Email { get; set; }
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
            });
            Validator<LoginUserRequestValidator>();
        }

            private readonly IAuthService _authService;

        public LoginUserEndpoint(IAuthService authService)
        {
            _authService = authService;
        }

        public override async Task HandleAsync(LoginUserRequest req, CancellationToken ct)
        {
            AccessToken token = await _authService.LoginAsync(req.Email,req.Password);

            LoginUserResponse response = new(){AccessToken=token};

            await SendAsync(response);
        }
    }

    public class LoginUserResponse
    {
        public AccessToken AccessToken { get; set; }
    }
}