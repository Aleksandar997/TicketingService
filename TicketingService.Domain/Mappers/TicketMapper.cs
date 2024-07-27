using TicketingService.DataSource.Ticketing;
using TicketingService.Domain.Entities;

namespace TicketingService.Domain.Mappers
{
    public static class TicketMapper
    {
        public static TicketEntity DataToEntity(Ticket ticket)
        {
            return new TicketEntity()
            {
                Id = ticket.Id,
                Description = ticket.Description,
                CustomerId = ticket.CustomerId,
                TicketStatus = ticket.TicketStatus
            };
        }

        public static IEnumerable<TicketEntity> DataToEntityListAsync(IEnumerable<Ticket> ticket) => 
            ticket.Select(DataToEntity);
    }
}
