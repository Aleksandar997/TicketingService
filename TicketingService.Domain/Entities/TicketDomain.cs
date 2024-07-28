namespace TicketingService.Domain.Entities
{
    public class TicketDomain
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public Guid CustomerId { get; set; }
        public required TicketStatusDomain TicketStatus { get; set; }
    }
}
