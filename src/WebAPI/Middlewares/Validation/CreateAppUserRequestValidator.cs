﻿using FastEndpoints;
using FluentValidation;
using WebAPI.Contracts.Requests.AppUser;

namespace WebAPI.Middlewares.Validation
{
    public sealed class CreateAppUserRequestValidator : Validator<CreateAppUserRequest>
    {
        public CreateAppUserRequestValidator()
        {
            RuleFor(r => r.Email).NotEmpty().EmailAddress().WithMessage("You must enter a valid email");
            RuleFor(r => r.FirstName).NotEmpty().MinimumLength(3);
            RuleFor(r => r.LastName).NotEmpty().WithMessage("You must enter your last name");
            RuleFor(r => r.UserName).NotEmpty().MinimumLength(3).WithMessage("Minimum user name length is three");
            RuleFor(r => r.PhoneNumber).NotEmpty();
            RuleFor(r => r.Password).NotEmpty().MinimumLength(3).WithMessage("Minimum password length is three");
        }
    }
}