namespace WebAPI.Contracts.Requests.AppUser
{
    public sealed class CreateAppUserRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

    }
}
