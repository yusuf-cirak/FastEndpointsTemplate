using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Endpoints.Auth;

namespace WebAPI.Extensions
{
    public static class BusinessRuleServices
    {
        public static IServiceCollection AddBusinessRulesServices(this IServiceCollection services){
            services.AddScoped<AuthBusinessRules>();

            return services;
        }
    }
}