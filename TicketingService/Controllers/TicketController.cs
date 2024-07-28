using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TicketingService.API.Extensions;
using TicketingService.Domain.Entities;
using TicketingService.Domain.Handlers;

namespace TicketingService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController
    {
        private readonly ILogger<TicketController> _logger;
        private readonly IGetAllHandler<TicketDomain> _getAllHandler;

        public TicketController(IGetAllHandler<TicketDomain> getAllHandler, ILogger<TicketController> logger)
        {
            _logger = logger;
            _getAllHandler = getAllHandler;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string SortBy, [FromQuery] SortOrder SortOrder, [FromQuery] int PageSize, [FromQuery] int PageNumber) =>
            await _getAllHandler.Handle(new Domain.Common.PagingDomain()
            {
                SortBy = SortBy,
                SortOrder = SortOrder,
                PageSize = PageSize,
                PageNumber = PageNumber
            }).ToAutoResponse();
    }
}
