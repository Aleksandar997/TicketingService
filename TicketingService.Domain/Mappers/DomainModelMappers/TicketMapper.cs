using TicketingService.DataSource.Ticketing;
using TicketingService.Domain.Entities;

namespace TicketingService.Domain.Mappers.DomainModelMappers
{
    public static class TicketMapper
    {
        public static TicketDomain DataSourceToDomain(Ticket ticket)
        {
            return new TicketDomain()
            {
                Id = ticket.Id,
                Description = ticket.Description,
                CustomerId = ticket.CustomerId,
                TicketStatus = TicketStatusMapper.DataSourceToDomain(ticket.TicketStatus)
            };
        }

        public static IEnumerable<TicketDomain> DataSourceToDomain(IEnumerable<Ticket> ticket) =>
            ticket.Select(DataSourceToDomain);
    }
}
