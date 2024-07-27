using MediatR;
using Microsoft.Data.SqlClient;

namespace TicketingService.Domain.Common
{
    public interface IPaging
    {
        string SortBy { get; set; }
        SortOrder SortOrder { get; set; }
        int PageSize { get; set; }
        int PageNumber { get; set; }
    }
    public interface IPagableRequest<out TResponse> : IRequest<TResponse>, IPaging
    {

    }
}