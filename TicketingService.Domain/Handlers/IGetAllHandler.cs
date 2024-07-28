using TicketingService.Domain.Common;

namespace TicketingService.Domain.Handlers
{
    public interface IGetAllHandler<T>
    {
        Task<ResponseBase<IEnumerable<T>>> Handle(PagingDomain pagingQuery);
    }
}
