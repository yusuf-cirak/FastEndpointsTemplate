using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using FluentValidation;

namespace WebAPI.Endpoints.CreateOperationClaim
{
    public sealed class CreateOperationClaimValidator:Validator<CreateOperationClaimRequest>
    {

        public CreateOperationClaimValidator()
        {
            RuleFor(e=>e.Name).NotEmpty().MinimumLength(2).WithMessage("Minimum operation claim length is two");
        }
        
    }
}