using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Repositories.User;
using WebAPI.Data.Services.Auth;
using WebAPI.Helpers.Token;

namespace WebAPI.Extensions
{
    public static class RepositoryServices
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHandler,TokenHandler>();

            services.AddScoped<IUserRepository,UserRepository>();

            services.AddScoped<IUserService,UserService>();

            services.AddScoped<IAuthService,AuthService>();




            return services;
        }
    }
}