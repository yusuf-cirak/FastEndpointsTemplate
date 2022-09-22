using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Extensions
{
    public static class HelpersServices
    {
        public static IServiceCollection AddHelpersServices(this IServiceCollection services)
        {
            // services.AddScoped<TokenHandler>();
            return services;
        }
    }
}