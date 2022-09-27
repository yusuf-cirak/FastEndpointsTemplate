using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using FluentValidation;
using WebAPI.Models;

namespace WebAPI.Endpoints.UpdateUser
{
    public sealed class UpdateUserValidator:Validator<UpdateUserRequest>
    {
        public UpdateUserValidator()
        {
            RuleFor(e=>e.Id).NotEmpty().Length(36).WithMessage("Incorrect id format");
        }
    }
}