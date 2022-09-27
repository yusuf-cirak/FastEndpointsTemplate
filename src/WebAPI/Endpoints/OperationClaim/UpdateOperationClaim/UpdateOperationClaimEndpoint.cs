using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using WebAPI.Data.Repositories;
using WebAPI.Mapping;
using WebAPI.Models;

namespace WebAPI.Endpoints.UpdateOperationClaim
{
    public sealed class UpdateOperationClaimRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    
    public sealed class UpdateOperationClaimEndpoint:Endpoint<UpdateOperationClaimRequest,UpdateOperationClaimResponse,UpdateOperationClaimMapper>
    {
      public override void Configure(){

        Put("operationclaim");
        Roles("Admin");

        Summary(s=>{
                s.Summary="Update a operation claim";
                s.Description="Update a operation claim with parameters below";
            });

        Validator<UpdateOperationClaimValidator>();
       }

       private readonly IOperationClaimRepository _repository;
       private readonly OperationClaimBusinessRules _businessRules;

        public UpdateOperationClaimEndpoint(IOperationClaimRepository repository, OperationClaimBusinessRules businessRules)
        {
            _repository = repository;
            _businessRules = businessRules;
        }

        public override async Task HandleAsync(UpdateOperationClaimRequest req, CancellationToken ct)
        {

            OperationClaim operationClaim= await _businessRules.OperationClaimMustExistBeforeUpdated(req.Id);

           await _businessRules.OperationClaimNameCannotBeDuplicatedBeforeCreatedOrUpdated(req.Name);

            operationClaim.Name=req.Name;

            operationClaim=await _repository.UpdateAsync(operationClaim);

            await SendAsync(Map.ToResponseEntity(operationClaim));
        }
        
    }


    public sealed class UpdateOperationClaimResponse
    {
        public OperationClaim UpdatedOperationClaim { get; set; }
    }
}