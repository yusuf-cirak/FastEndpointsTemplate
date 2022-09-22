using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Contexts;
using WebAPI.Data.Repositories;
using WebAPI.Models;

namespace WebAPI.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddDbContextServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Context
            services.AddDbContext<TemplateDbContext>
                (options => options.UseSqlServer(configuration.GetConnectionString("TemplateSqlServer")));

            // Identity
            services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 3;
                opt.Password.RequireNonAlphanumeric = false;
                opt.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<TemplateDbContext>();

            return services;
        }

        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {

            services.AddScoped<AppUserRepository>();

            return services;
        }
    }
}
