using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SMS_Auth.Infrastructure
{
    public static class InfrastructureContainer
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
        {
            string? conn = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(option => option.UseSqlServer(conn));
            return services;
        }
    }
}
