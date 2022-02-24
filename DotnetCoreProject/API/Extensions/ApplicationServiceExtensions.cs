using API.Database;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Inject Token Service
            services.AddScoped<ITokenService, TokenService>();
            
            //inject Repository service
            services.AddScoped<IUserRepository, UserRepository>();

            //Lamda expression to create a db connection or DB injection
            //services.AddDbContext<DatabaseContext>(options => options.UseSqlServer("Connection string that should come from appSettings as well defined below"));
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServerConnectionString")));

            return services;            
        }
    }
}