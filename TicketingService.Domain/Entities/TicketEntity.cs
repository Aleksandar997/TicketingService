using TicketingService.DataSource.Ticketing;
using TicketingService.Domain.Entities.Abstract;

namespace TicketingService.Domain.Entities
{
    public class TicketEntity : IRespondable
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public Guid CustomerId { get; set; }
        public required TicketStatus TicketStatus { get; set; }
    }
}
