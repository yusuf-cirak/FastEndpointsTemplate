using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using WebAPI.Data.Services.Auth;
using WebAPI.Mapping;

namespace WebAPI.Endpoints.GetUserById
{
    public sealed class GetUserByIdRequest
    {
        public string Id { get; set; }
    }

    public sealed class GetUserByIdEndpoint : Endpoint<GetUserByIdRequest, GetUserByIdResponse,GetUserByIdMapper>
    {
        public override void Configure()
        {
            Get("user/{Id}");
            Roles("Admin", "Moderator");
            Summary(s =>
            {
                s.Summary = "Get a user with id";
                s.Description = "Get user with parameter below";
            });
        }

        readonly IUserService _userService;

        public GetUserByIdEndpoint(IUserService userService)
        {
            _userService = userService;
        }

        public override async Task HandleAsync(GetUserByIdRequest req, CancellationToken ct)
        {
            var user = await _userService.GetUserById(req.Id, includeOperationClaims: false);

            await SendAsync(Map.ToResponseEntity(user));
        }
    }

    public sealed class GetUserByIdResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
    }
}