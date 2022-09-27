using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using WebAPI.Endpoints.DeleteUser;
using WebAPI.Models;

namespace WebAPI.Mapping
{
    public sealed class DeleteUserMapper:Mapper<DeleteUserRequest,DeleteUserResponse,User>
    {

        public DeleteUserResponse ToResponseEntity(User user)
        =>new(){UserName=user.UserName,Email=user.Email,Id=user.Id};
    }
}