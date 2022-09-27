using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using WebAPI.Data.Services.Auth;
using WebAPI.Mapping;
using WebAPI.Models;
using WebAPI.Utilities.Security.Hashing;

namespace WebAPI.Endpoints.UpdateUser
{
    public sealed class UpdateUserRequest
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }


    public sealed class UpdateUserEndpoint:Endpoint<UpdateUserRequest,UpdateUserResponse,UpdateUserMapper>
    {
        public override void Configure()
        {
            Put("user");
            Roles("Admin","User");
            Summary(e=>{
                e.Summary="Update user credentials";
                e.Description="Update user with parameters below";
            });
        }

        private readonly IUserService _userService;

        public UpdateUserEndpoint(IUserService userService)
        {
            _userService = userService;
        }

        public override async Task HandleAsync(UpdateUserRequest req, CancellationToken ct)
        {
            User user= await _userService.GetUserById(req.Id,false);
            if (!string.IsNullOrEmpty(req.Password))
            {
                HashingHelper.CreatePasswordHash(req.Password,out byte[] passwordHash,out byte[] passwordSalt);
                user.PasswordSalt=passwordSalt;
                user.PasswordHash=passwordHash;
            }

            user=Map.ToUpdatedEntity(user,req);
            

            user=await _userService.UpdateUserAsync(user);


            await SendAsync(Map.ToResponseEntity(user));


        }
    }

    public sealed class UpdateUserResponse
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}