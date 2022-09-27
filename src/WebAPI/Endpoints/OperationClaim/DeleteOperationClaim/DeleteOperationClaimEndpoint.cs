using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using WebAPI.Data.Repositories;
using WebAPI.Mapping;
using WebAPI.Models;

namespace WebAPI.Endpoints.DeleteOperationClaim
{
    public sealed class DeleteOperationClaimRequest
    {
        public string Id { get; set; }
    }

    public sealed class DeleteOperationClaimEndpoint:Endpoint<DeleteOperationClaimRequest,DeleteOperationClaimResponse,DeleteOperationClaimMapper>
    {
        public override void Configure()
        {
            Delete("operationclaim");
            Roles("Admin");
            Summary(s=>{
                s.Summary="Delete a operation claim";
                s.Description="Delete a operation claim with id";
            });

            Validator<DeleteOperationClaimValidator>();
        }

        private readonly IOperationClaimRepository _repository;
        private readonly OperationClaimBusinessRules _businessRules;

        public DeleteOperationClaimEndpoint(OperationClaimBusinessRules businessRules, IOperationClaimRepository repository)
        {
            _businessRules = businessRules;
            _repository = repository;
        }

        public override async Task HandleAsync(DeleteOperationClaimRequest req, CancellationToken ct)
        {

            OperationClaim operationClaim=await _businessRules.OperationClaimMustExistBeforeDeleted(req.Id);

           await _repository.DeleteAsync(operationClaim);

           await SendAsync(Map.ToResponseEntity(operationClaim));
        }
    }

    public sealed class DeleteOperationClaimResponse
    {
        public OperationClaim DeletedOperationClaim { get; set; }
    }
}