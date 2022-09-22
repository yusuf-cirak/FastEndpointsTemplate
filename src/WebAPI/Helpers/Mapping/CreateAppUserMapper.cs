using FastEndpoints;
using WebAPI.Contracts.Requests.AppUser;
using WebAPI.Contracts.Responses.AppUser;
using WebAPI.Models;

namespace WebAPI.Helpers.Mapping
{
    public sealed class CreateAppUserMapper : Mapper<CreateAppUserRequest, CreateAppUserResponse, AppUser>
    {
        public override AppUser ToEntity(CreateAppUserRequest r) => new()
        {
            Email = r.Email,
            PhoneNumber = r.PhoneNumber,
            UserName = r.UserName,
            FirstName = r.FirstName,
            LastName = r.LastName
        };

        public override CreateAppUserResponse FromEntity(AppUser u) => new()
        {
            Id = u.Id,
            Email = u.Email,
            FirstName = u.FirstName,
            LastName = u.LastName,
            PhoneNumber = u.PhoneNumber,
            UserName = u.UserName
        };
    }
}
