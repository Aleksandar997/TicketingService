using System.ComponentModel.DataAnnotations;

namespace TicketingService.DataSource.Ticketing
{
    public class TicketStatus
    {
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string? Value { get; set; }
        public byte Order { get; set; }
    }
}
