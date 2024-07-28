using Microsoft.Extensions.DependencyInjection;
using TicketingService.DataSource.Ticketing.Repository.Interfacecs;
using TicketingService.DataSource.Ticketing.Repository;
using TicketingService.DataSource.Ticketing;
using TicketingService.Domain.Handlers;
using TicketingService.Domain.Entities;
using TicketingService.Domain.Handlers.Implementations;

namespace TicketingService.Domain
{
    public static class ServiceConfiguration
    {
        public static void ConfigureDomainServices(this IServiceCollection services, string connectionString)
        {
            services.ConfigureDataSourceServices(connectionString);
            services.AddScoped<IGetAllHandler<TicketDomain>, GetAllTicketsHandler>();
            services.AddScoped<ITicketRepository, TicketRepository>();
        }
    }
}
