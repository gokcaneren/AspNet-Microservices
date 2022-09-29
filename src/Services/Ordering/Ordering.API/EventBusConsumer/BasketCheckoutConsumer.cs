using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Features.Commands.CheckoutOrder;
using System.Threading.Tasks;

namespace Ordering.API.EventBusConsumer
{
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BasketCheckoutConsumer> _logger;
        private readonly IMapper _mapper;
        public BasketCheckoutConsumer(IMediator mediator, ILogger<BasketCheckoutConsumer> logger, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            var command = _mapper.Map<CheckoutOrderCommand>(context.Message);
            var result = await _mediator.Send(command);

            _logger.LogInformation("BasketCheckoutEvent consumed succesfully! Created Order Id : {newOrderId}", result);
        }
    }
}
