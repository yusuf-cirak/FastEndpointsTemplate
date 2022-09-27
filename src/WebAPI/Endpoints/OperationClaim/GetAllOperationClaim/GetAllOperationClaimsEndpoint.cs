using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using WebAPI.Data.Repositories;
using WebAPI.Models;

namespace WebAPI.Endpoints.GetAllOperationClaim
{
    public sealed class GetAllOperationClaimsEndpoint:EndpointWithoutRequest<GetAllOperationClaimsResponse>
    {
        public override void Configure()
        {
            Get("operationclaim");
            Roles("Admin");
            Summary(s=>{
                s.Summary="Get all operation claims";
            });
        }


        private readonly IOperationClaimRepository _repository;

        public GetAllOperationClaimsEndpoint(IOperationClaimRepository repository)
        {
            _repository = repository;
        }


        public async override Task HandleAsync(CancellationToken ct)
        {
           var operationClaims=await _repository.GetListAsync();


           var response=new GetAllOperationClaimsResponse{OperationClaims=operationClaims};


           await SendAsync(response);
        }
    }


    public sealed class GetAllOperationClaimsResponse
    {
        public IList<OperationClaim> OperationClaims { get; set; }
    }
}