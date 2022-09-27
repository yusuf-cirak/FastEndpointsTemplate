using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Contexts;

namespace WebAPI.Extensions
{
    public static class DatabaseRegistration
    {
        public static IServiceCollection AddDbContextServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Context
            services.AddDbContext<TemplateDbContext>
                (options => options.UseSqlServer(configuration.GetConnectionString("TemplateSqlServer")));

            return services;
        }
    }
}