using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using WebAPI.Endpoints.CreateOperationClaim;
using WebAPI.Models;

namespace WebAPI.Mapping
{
    public sealed class CreateOperationClaimMapper:Mapper<CreateOperationClaimRequest,CreateOperationClaimResponse,OperationClaim>{
        public override OperationClaim ToEntity(CreateOperationClaimRequest r)
        =>new(){
            Name=r.Name
        };

        public CreateOperationClaimResponse ToResponseEntity(OperationClaim operationClaim)
        =>new(){CreatedOperationClaim=operationClaim};
    }
}