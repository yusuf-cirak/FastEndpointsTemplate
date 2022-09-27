using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FastEndpoints;
using WebAPI.Data.Repositories;
using WebAPI.Mapping;
using WebAPI.Models;

namespace WebAPI.Endpoints.CreateOperationClaim
{
   public sealed class CreateOperationClaimRequest
    {
        public string Name { get; set; }
    }

    
    public sealed class CreateOperationClaimEndpoint:Endpoint<CreateOperationClaimRequest,CreateOperationClaimResponse,CreateOperationClaimMapper>
    {
      public override void Configure(){

        Post("operationclaim");
        Roles("Admin");
        Summary(s =>
            {
                s.Summary = "Create a operation claim";
                s.Description = "Create operation claim with parameters below";
            });

        Validator<CreateOperationClaimValidator>();
       }

       private readonly IOperationClaimRepository _repository;
       private readonly OperationClaimBusinessRules _businessRules;

        public CreateOperationClaimEndpoint(IOperationClaimRepository repository, OperationClaimBusinessRules businessRules)
        {
            _repository = repository;
            _businessRules = businessRules;
        }

        public override async Task HandleAsync(CreateOperationClaimRequest req, CancellationToken ct)
        {

           await _businessRules.OperationClaimNameCannotBeDuplicatedBeforeCreatedOrUpdated(req.Name);

            var operationClaim=await _repository.AddAsync(new(id:Guid.NewGuid().ToString(),req.Name));

            await SendAsync(Map.ToResponseEntity(operationClaim));

        }
        
    }


    public sealed class CreateOperationClaimResponse
    {
        public OperationClaim CreatedOperationClaim { get; set; }
    }
}