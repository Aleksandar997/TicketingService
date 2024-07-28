using TicketingService.DataSource.Ticketing;
using TicketingService.Domain.Entities;

namespace TicketingService.Domain.Mappers.DomainModelMappers
{
    public static class TicketStatusMapper
    {
        public static TicketStatusDomain DataSourceToDomain(TicketStatus ticketStatus)
        {
            return new TicketStatusDomain()
            {
                Id = ticketStatus.Id,
                Value = ticketStatus.Value,
                Order = ticketStatus.Order
            };
        }
    }
}
