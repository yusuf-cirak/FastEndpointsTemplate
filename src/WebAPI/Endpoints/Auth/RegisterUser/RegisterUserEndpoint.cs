using FastEndpoints;
using WebAPI.Data.Repositories;
using WebAPI.Data.Services.Auth;
using WebAPI.Helpers.Mapping;
using WebAPI.Helpers.Token;
using WebAPI.Middlewares.Validation;
using WebAPI.Models;

namespace WebAPI.Endpoints.Auth
{
    public sealed class RegisterUserRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

    }

    public sealed class RegisterUserEndpoint : Endpoint<RegisterUserRequest, RegisterUserResponse, RegisterUserMapper>
    {
        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("auth/register");
            AllowAnonymous();
            Summary(s => {
                s.Summary = "Register a user";
                s.Description = "Register with parameters below";
            });
            Validator<RegisterUserRequestValidator>();

        }


        private readonly IAuthService _authService;


        public RegisterUserEndpoint(IAuthService authService)
        {
            _authService = authService;
        }


        public override async Task HandleAsync(RegisterUserRequest request, CancellationToken cancellationToken)
        {

            User user = Map.ToEntity(request);

           AccessToken token= await _authService.RegisterAsync(user,request.Password);

           RegisterUserResponse response=new(){AccessToken=token};
            await SendAsync(response);
        }
    }

    public sealed class RegisterUserResponse
    {
        public AccessToken AccessToken { get; set; }
    }
}
