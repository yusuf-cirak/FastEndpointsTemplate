using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers.Token;

namespace WebAPI.Extensions
{
    public static class HelperServices
    {
        public static IServiceCollection AddHelpersServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHandler,TokenHandler>();
            return services;
        }
    }
}