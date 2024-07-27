using System.ComponentModel.DataAnnotations;

namespace TicketingService.DataSource.Ticketing.Common
{
    public abstract class Meta
    {
        public required DateTime MetaDateCreated { get; set; }
        [MaxLength(20)]
        public required string MetaCreatedBy { get; set; }
        public DateTime? MetaDateUpdated { get; set; }
        [MaxLength(20)]
        public string? MetaUpdatedBy { get; set; }
    }
}
