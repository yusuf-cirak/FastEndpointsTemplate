using FastEndpoints;
using WebAPI.Endpoints;
using WebAPI.Endpoints.Auth;
using WebAPI.Endpoints.OperationClaim.CreateOperationClaim;
using WebAPI.Models;

namespace WebAPI.Helpers.Mapping
{
    public sealed class RegisterUserMapper : Mapper<RegisterUserRequest, RegisterUserResponse, User>
    {
        public override User ToEntity(RegisterUserRequest r) => new()
        {
            FirstName=r.FirstName,
            LastName=r.LastName,
            Email=r.Email,
            UserName=r.UserName
            
        };
    }


    public sealed class CreateOperationClaimMapper:Mapper<CreateOperationClaimRequest,CreateOperationClaimResponse,OperationClaim>{
        public override OperationClaim ToEntity(CreateOperationClaimRequest r)
        =>new(){
            Name=r.Name
        };
    }
}