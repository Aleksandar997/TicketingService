using MediatR;
using TicketingService.Domain.Entities;
using TicketingService.Domain.Mappers;
using TicketingService.DataSource.Ticketing.Repository.Interfacecs;
using TicketingService.Domain.Common;
using Microsoft.Data.SqlClient;

namespace TicketingService.Domain
{
    public class GetTickets
    {
        public class Query : IPagableRequest<ResponseBase<IEnumerable<TicketEntity>>>
        {
            public required string SortBy { get; set; }
            public SortOrder SortOrder { get; set; }
            public int PageSize { get; set; }
            public int PageNumber { get; set; }
        }

        public class QueryHandlerAsync : IRequestHandler<Query, ResponseBase<IEnumerable<TicketEntity>>>
        {
            private readonly ITicketRepository _ticketRepository;

            public QueryHandlerAsync(ITicketRepository ticketRepository)
            {
                _ticketRepository = ticketRepository;
            }

            public async Task<ResponseBase<IEnumerable<TicketEntity>>> Handle(Query request, CancellationToken cancellationToken) =>
                await _ticketRepository.GetAll(request.ToDataSourcePaging()).ToResponseBase(TicketMapper.DataToEntityListAsync);
        }
    }
}