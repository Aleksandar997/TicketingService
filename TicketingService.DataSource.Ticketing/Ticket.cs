using System.ComponentModel.DataAnnotations;
using TicketingService.DataSource.Ticketing.Common;

namespace TicketingService.DataSource.Ticketing
{
    public class Ticket : Meta
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        public Guid CustomerId { get; set; }
        public Guid TicketStatusId { get; set; }
        public virtual required TicketStatus TicketStatus { get; set; }
    }
}
