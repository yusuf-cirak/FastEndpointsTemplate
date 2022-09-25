using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FastEndpoints;
using WebAPI.Data.Repositories;
using WebAPI.Mapping;

namespace WebAPI.Endpoints.OperationClaim.CreateOperationClaim
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
       }

       private readonly IOperationClaimRepository _repository;

        public CreateOperationClaimEndpoint(IOperationClaimRepository repository)
        {
            _repository = repository;
        }

        public override async Task HandleAsync(CreateOperationClaimRequest req, CancellationToken ct)
        {

            var operationClaim=await _repository.AddAsync(Map.ToEntity(req));

            CreateOperationClaimResponse response = new(){OperationClaim=operationClaim};

            await SendAsync(response);
        }
        
    }


    public sealed class CreateOperationClaimResponse
    {
        public Models.OperationClaim OperationClaim { get; set; }
    }
}