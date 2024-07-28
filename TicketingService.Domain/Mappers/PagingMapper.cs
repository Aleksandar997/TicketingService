using TicketingService.DataSource.Ticketing.Common;
using TicketingService.Domain.Common;

namespace TicketingService.Domain.Mappers
{
    public static class PagingMapper
    {
        public static PagingDataSource ToPagingDataSource(this PagingDomain paging)
        {
            return new PagingDataSource()
            { 
                SortBy = paging.SortBy, 
                SortOrder = paging.SortOrder == Microsoft.Data.SqlClient.SortOrder.Ascending ? "ASC" : "DESC", 
                PageNumber = paging.PageNumber, 
                PageSize = paging.PageSize 
            };
        }
    }
}
