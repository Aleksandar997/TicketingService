using TicketingService.DataSource.Ticketing.Repository.Interfacecs;
using TicketingService.Domain.Common;
using TicketingService.Domain.Mappers;
using TicketingService.Domain.Entities;
using TicketingService.Domain.Mappers.DomainModelMappers;

namespace TicketingService.Domain.Handlers.Implementations
{
    public class GetAllTicketsHandler : IGetAllHandler<TicketDomain>
    {
        private readonly ITicketRepository _ticketRepository;

        public GetAllTicketsHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
        public async Task<ResponseBase<IEnumerable<TicketDomain>>> Handle(PagingDomain pagingQuery)
        {
            return await _ticketRepository.GetAll(pagingQuery.ToPagingDataSource()).ToResponseBase(TicketMapper.DataSourceToDomain);
        }
            
    }
}
