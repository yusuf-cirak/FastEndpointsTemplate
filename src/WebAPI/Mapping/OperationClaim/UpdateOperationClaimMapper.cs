using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using WebAPI.Endpoints.UpdateOperationClaim;
using WebAPI.Models;

namespace WebAPI.Mapping
{
    public sealed class UpdateOperationClaimMapper:Mapper<UpdateOperationClaimRequest,UpdateOperationClaimResponse,OperationClaim>{
        public override OperationClaim ToEntity(UpdateOperationClaimRequest r)
        =>new(){
            Name=r.Name
        };

        public UpdateOperationClaimResponse ToResponseEntity(OperationClaim operationClaim)
        =>new(){UpdatedOperationClaim=operationClaim};
    }
}