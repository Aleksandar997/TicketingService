using Framework.DataSource;
using Framework.DataSource.DbConnector;
using TicketingService.DataSource.Ticketing.Common;
using TicketingService.DataSource.Ticketing.Repository.Interfacecs;

namespace TicketingService.DataSource.Ticketing.Repository
{
    public class TicketRepository : ITicketRepository
    {
        IDbConnector _idbConnector;
        public TicketRepository(IDbConnector idbConnector)
        {
            _idbConnector = idbConnector;
        }
        public async Task<DataSourceResponse<IEnumerable<Ticket>>> GetAll(Paging paging)
        {
            var data = await _idbConnector
                .QueryMultipleAsync()
                .AddParameters(paging)
                .ExecuteAsync(
                    "public.get_all_tickets",
                    SqlCommandType.Function,
                    (reader) =>
                    {
                        return reader.Read<Ticket, TicketStatus, Ticket>((ticket, ticketStatus) =>
                        {
                            ticket.TicketStatus = ticketStatus;
                            return ticket;
                        }, splitOn: "TicketStatusId");
                    }
                );

            var count = await _idbConnector
                .QuerySingleAsync()
                .ExecuteAsync<int>("public.get_all_tickets_count", SqlCommandType.Function);

            return DataSourceResponse<IEnumerable<Ticket>>.Create(data, count);
        }
    }
}
