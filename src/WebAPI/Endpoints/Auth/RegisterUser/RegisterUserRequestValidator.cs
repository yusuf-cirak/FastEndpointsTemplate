using FastEndpoints;
using FluentValidation;
using WebAPI.Endpoints;
using WebAPI.Endpoints.Auth;

namespace WebAPI.Middlewares.Validation
{
    public sealed class RegisterUserRequestValidator : Validator<RegisterUserRequest>
    {
        public RegisterUserRequestValidator()
        {
            RuleFor(r => r.Email).NotEmpty().EmailAddress().WithMessage("You must enter a valid email");
            RuleFor(r => r.FirstName).NotEmpty().MinimumLength(3);
            RuleFor(r => r.LastName).NotEmpty().WithMessage("You must enter your last name");
            RuleFor(r => r.UserName).NotEmpty().MinimumLength(3).WithMessage("Minimum user name length is three");
            RuleFor(r => r.PhoneNumber).NotEmpty().MinimumLength(13).WithMessage("Wrong number format, example format is 000-000-00-00").NotEqual("000-000-00-00");
            RuleFor(r => r.Password).NotEmpty().MinimumLength(3).WithMessage("Minimum password length is three");
        }
    }
}
