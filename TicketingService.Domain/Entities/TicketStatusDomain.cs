namespace TicketingService.Domain.Entities
{
    public class TicketStatusDomain
    {
        public Guid Id { get; set; }
        public string? Value { get; set; }
        public byte Order { get; set; }
    }
}
