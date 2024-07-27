using Framework.DataSource;
using Microsoft.Extensions.DependencyInjection;
using TicketingService.DataSource.Ticketing.Repository;
using TicketingService.DataSource.Ticketing.Repository.Interfacecs;

namespace TicketingService.DataSource.Ticketing
{
    public static class ServiceConfiguration
    {
        public static void ConfigureDataSourceServices(this IServiceCollection services, string connectionString)
        {
            services.ConfigurePostgreDapper(connectionString);
            services.AddScoped<ITicketRepository, TicketRepository>();
        }
    }
}
