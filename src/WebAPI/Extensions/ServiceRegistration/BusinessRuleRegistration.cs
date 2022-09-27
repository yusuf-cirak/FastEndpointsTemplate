using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Endpoints;
using WebAPI.Endpoints.Auth;

namespace WebAPI.Extensions
{
    public static class BusinessRuleRegistration
    {
        public static IServiceCollection AddBusinessRuleServices(this IServiceCollection services){
            services.AddScoped<AuthBusinessRules>();

            services.AddScoped<OperationClaimBusinessRules>();

            services.AddScoped<UserBusinessRules>();

            return services;
        }
    }
}