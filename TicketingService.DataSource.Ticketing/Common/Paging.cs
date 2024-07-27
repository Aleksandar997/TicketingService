using Microsoft.Data.SqlClient;

namespace TicketingService.DataSource.Ticketing.Common
{
    public class Paging
    {
        public required string SortBy { get; set; }
        public required string SortOrder { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
