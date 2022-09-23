using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Repositories;
using WebAPI.Data.Repositories;
using WebAPI.Data.Services.Auth;
using WebAPI.Helpers.Token;

namespace WebAPI.Extensions
{
    public static class RepositoryServices
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository,UserRepository>();

            services.AddScoped<IUserService,UserService>();

            services.AddScoped<IAuthService,AuthService>();

            services.AddScoped<IOperationClaimRepository,OperationClaimRepository>();


            services.AddScoped<IUserOperationClaimRepository,UserOperationClaimRepository>();

            return services;
        }
    }
}