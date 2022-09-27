using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using WebAPI.Endpoints.GetUserById;
using WebAPI.Models;

namespace WebAPI.Mapping
{
    public sealed class GetUserByIdMapper:Mapper<GetUserByIdRequest,GetUserByIdResponse,User>
    {
        public GetUserByIdResponse ToResponseEntity(User e)
        => new(){
            FirstName=e.FirstName,
            Email=e.Email,
            LastName=e.LastName,
            Status=e.Status,
            UserName=e.UserName
        };
    }
}