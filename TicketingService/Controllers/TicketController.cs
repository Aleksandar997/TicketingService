using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TicketingService.Domain;

namespace TicketingService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : ControllerBaseAdapter
    {
        private readonly ILogger<TicketController> _logger;

        public TicketController(IMediator mediator, ILogger<TicketController> logger) : base(mediator)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string SortBy, [FromQuery] SortOrder SortOrder, [FromQuery] int PageSize, [FromQuery] int PageNumber) =>
            await HandleRequest(new GetTickets.Query()
            {
                SortBy = SortBy,
                SortOrder = SortOrder,
                PageSize = PageSize,
                PageNumber = PageNumber
            });
    }
}
