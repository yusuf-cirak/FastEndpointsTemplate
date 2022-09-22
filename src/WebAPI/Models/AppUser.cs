using Microsoft.AspNetCore.Identity;

namespace WebAPI.Models
{
    public sealed class AppUser:IdentityUser<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
