using Microsoft.Data.SqlClient;

namespace TicketingService.Domain.Common
{
    public class PagingDomain
    {
        public required string SortBy { get; set; }
        public SortOrder SortOrder { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}