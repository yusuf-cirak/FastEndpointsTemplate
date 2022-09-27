using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using FluentValidation;
using WebAPI.Endpoints.GetUserById;

namespace WebAPI.Endpoints.GetUserById
{
    public sealed class GetUserByIdValidator:Validator<GetUserByIdRequest>
    {
        public GetUserByIdValidator()
        {
            RuleFor(e=>e.Id).NotEmpty().Length(36).WithMessage("Incorrect id format"); // Id length is 36
        }
    }
}