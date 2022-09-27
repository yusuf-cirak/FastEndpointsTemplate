using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using WebAPI.Endpoints.DeleteOperationClaim;
using WebAPI.Models;

namespace WebAPI.Mapping
{
    public sealed class DeleteOperationClaimMapper:Mapper<DeleteOperationClaimRequest,DeleteOperationClaimResponse,OperationClaim>
    {
        public DeleteOperationClaimResponse ToResponseEntity(OperationClaim operationClaim)
        =>new(){DeletedOperationClaim=operationClaim};
        
    }
}