using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using WebAPI.Data.Services.Auth;
using WebAPI.Mapping;
using WebAPI.Models;

namespace WebAPI.Endpoints.DeleteUser
{
    public sealed class DeleteUserRequest
    {
        [FromHeader("UserId")]
        public string Id { get; set; }
    }


    public sealed class DeleteUserEndpoint:Endpoint<DeleteUserRequest,DeleteUserResponse,DeleteUserMapper>
    {
        public override void Configure()
        {
            Delete("user");
            Roles("User");
            Summary(s=>{
                s.Summary="Delete user by sending user id";
                s.Description="This process is irreversible";
            });
            Validator<DeleteUserRequestValidator>();
        }

        private readonly IUserService _userService;
        private readonly UserBusinessRules _businessRules;

        public DeleteUserEndpoint(IUserService userService, UserBusinessRules businessRules)
        {
            _userService = userService;
            _businessRules = businessRules;
        }

        public async override Task HandleAsync(DeleteUserRequest req, CancellationToken ct)
        {
            User user= await _businessRules.UserShouldExistBeforeDeleted(req.Id);

            _businessRules.CheckUserCredentials(req.Id); // User credentials must match or token should have "Admin" claim.

           await _userService.DeleteUserAsync(user);

           await SendAsync(Map.ToResponseEntity(user));

        }
    }

    public sealed class DeleteUserResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

    }

}