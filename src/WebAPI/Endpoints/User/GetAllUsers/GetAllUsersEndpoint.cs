using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Services.Auth;
using WebAPI.Mapping;
using WebAPI.Models;
using WebAPI.Utilities.Security.Extensions;

namespace WebAPI.Endpoints.GetAllUsers
{
    public sealed class GetAllUsersEndpoint:EndpointWithoutRequest<IList<GetAllUsersResponse>>
    {
        public override void Configure()
        {
            Get("user");
            Roles("Admin");
            Summary(s=>{
                s.Summary="Get all users";
            });
        }

        private readonly IUserService _userService;

        public GetAllUsersEndpoint(IUserService userService)
        {
            _userService = userService;
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            List<GetAllUsersResponse> response= await _userService.Query().AsNoTracking().Select(e=>new GetAllUsersResponse{
                Email=e.Email,
                FirstName=e.FirstName,
                LastName=e.LastName,
                UserName=e.UserName,
                Status=e.Status
            }).ToListAsync();

            await SendAsync(response);
        }
    }

    public sealed class GetAllUsersResponse
    {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public bool Status { get; set; }
    }
}