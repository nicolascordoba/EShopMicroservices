using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //var connection = configuration.GetConnectionString("Database");

            //services.AddDbContext<ApplicationDbContext>(options =>
            //{
            //    options.UseSqlServer(connection);
            //});

            //services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            return services;
        }
    }
}
