using Bitzen_API.ORM.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bitzen_API.Extensions
{
    public static class DatabaseExtension
    {
        public static IServiceCollection AddPostgresDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BitzenDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
