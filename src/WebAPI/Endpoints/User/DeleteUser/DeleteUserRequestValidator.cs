using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using FluentValidation;
using WebAPI.Endpoints.DeleteUser;

namespace WebAPI.Endpoints.DeleteUser
{
    public sealed class DeleteUserRequestValidator:Validator<DeleteUserRequest>
    {
        public DeleteUserRequestValidator()
        {
            RuleFor(e=>e.Id).NotEmpty().Length(36).WithMessage("Incorrect id format");
            // Id length is 36
        }
    }
}