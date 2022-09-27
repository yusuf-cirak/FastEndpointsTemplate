using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Utilities.JWT;

namespace WebAPI.Extensions
{
    public static class UtilityRegistration
    {
        public static IServiceCollection AddUtilityServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHandler,TokenHandler>();
            return services;
        }
    }
}