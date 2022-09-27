using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using WebAPI.Endpoints.UpdateUser;
using WebAPI.Models;

namespace WebAPI.Mapping
{
    public sealed class UpdateUserMapper:Mapper<UpdateUserRequest,UpdateUserResponse,User>
    {
        public User ToUpdatedEntity(User u, UpdateUserRequest r)
        =>new(){
            Email=r.Email,
            UserName=r.UserName,
            
            Id=u.Id,
            FirstName=u.FirstName,
            LastName=u.LastName,
            PasswordSalt=u.PasswordSalt,
            PasswordHash=u.PasswordHash,
            Status=u.Status
        };

        public UpdateUserResponse ToResponseEntity(User u)
        =>new(){
            Email=u.Email,
            FirstName=u.FirstName,
            LastName=u.LastName,
            UserName=u.UserName
        };
    }
}