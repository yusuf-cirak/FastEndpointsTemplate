using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using FluentValidation;
using WebAPI.Endpoints.Auth;

namespace WebAPI.Middlewares.Validation
{
    public sealed class LoginUserRequestValidator : Validator<LoginUserRequest>
    {
        public LoginUserRequestValidator()
        {
            RuleFor(r => r.EmailOrUserName).NotEmpty().WithMessage("You must enter a email or user name");
            RuleFor(r => r.Password).NotEmpty().MinimumLength(3).WithMessage("Minimum password length is three");
        }
    }
}