using FastEndpoints;
using WebAPI.Endpoints;
using WebAPI.Endpoints.Auth;
using WebAPI.Models;

namespace WebAPI.Helpers.Mapping
{
    public sealed class RegisterUserMapper : Mapper<RegisterUserRequest, RegisterUserResponse, User>
    {
        public override User ToEntity(RegisterUserRequest r) => new()
        {
            FirstName=r.FirstName,
            LastName=r.LastName,
            Email=r.Email
            
        };
    }
}