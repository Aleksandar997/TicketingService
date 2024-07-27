using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketingService.API.Extensions;
using TicketingService.Domain.Common;

namespace TicketingService.API.Controllers
{
    public class ControllerBaseAdapter : ControllerBase
    {
        public readonly IMediator _mediator;

        public ControllerBaseAdapter(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task<IActionResult> HandleRequest<TResponse>(IRequest<ResponseBase<TResponse>> request, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _mediator.Send(request, cancellationToken).ToAutoResponse();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
