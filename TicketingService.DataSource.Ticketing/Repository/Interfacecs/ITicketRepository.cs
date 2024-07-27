using TicketingService.DataSource.Ticketing.Common;

namespace TicketingService.DataSource.Ticketing.Repository.Interfacecs
{
    public interface ITicketRepository
    {
        public Task<DataSourceResponse<IEnumerable<Ticket>>> GetAll(Paging paging);
    }
}
