using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers.Token;
using WebAPI.Models;

namespace WebAPI.Data.Services.Auth
{
    public interface IAuthService
    {
        Task<AccessToken> RegisterAsync(User user,string password);

        Task<AccessToken> LoginAsync(string email, string password);

        
    }
}