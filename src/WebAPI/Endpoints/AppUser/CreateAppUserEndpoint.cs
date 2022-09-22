using FastEndpoints;
using WebAPI.Contracts.Requests.AppUser;
using WebAPI.Contracts.Responses.AppUser;
using WebAPI.Data.Repositories;
using WebAPI.Helpers.Mapping;
using WebAPI.Middlewares.Validation;
using WebAPI.Models;

namespace WebAPI.Endpoints
{
    public sealed class CreateAppUserEndpoint : Endpoint<CreateAppUserRequest, CreateAppUserResponse, CreateAppUserMapper>
    {
        public override void Configure()
        {
            Verbs(Http.POST);
            Routes("users");
            AllowAnonymous();
            Summary(s => {
                s.Summary = "Create user";
                s.Description = "Create user with the parameters below";
                s.Responses[200] = "User created successfully";
            });
            Validator<CreateAppUserRequestValidator>();

        }


        private readonly AppUserRepository _appUserRepository;

        public CreateAppUserEndpoint(AppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public override async Task HandleAsync(CreateAppUserRequest request, CancellationToken cancellationToken)
        {

            AppUser user = Map.ToEntity(request);
            user.Id = Guid.NewGuid().ToString();

            await _appUserRepository.CreateAppUserAsync(user, request.Password);

            await SendAsync(Map.FromEntity(user));
        }
    }
}
