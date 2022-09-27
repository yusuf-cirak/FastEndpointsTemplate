using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using FluentValidation;
using WebAPI.Endpoints.DeleteOperationClaim;

namespace WebAPI.Endpoints.DeleteOperationClaim
{
    public sealed class DeleteOperationClaimValidator:Validator<DeleteOperationClaimRequest>
    {
        public DeleteOperationClaimValidator()
        {
            RuleFor(e=>e.Id).NotEmpty().Length(36).WithMessage("Incorect id format");
        }
    }
}