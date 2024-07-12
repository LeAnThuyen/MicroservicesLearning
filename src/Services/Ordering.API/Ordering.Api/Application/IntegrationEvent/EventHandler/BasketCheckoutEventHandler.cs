

using AutoMapper;
using EventBus.Messages.IntegrationEvents.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Features.V1.Orders.Commands.CreateOrder;
using ILogger = Serilog.ILogger;
namespace Ordering.API.Application.IntegrationEvents.EventsHandler;

public class BasketCheckoutEventHandler:IConsumer<BasketCheckoutEvent>
{
    private IMediator _mediator;
    private ILogger _logger;
    private IMapper _mapper;
    
    public BasketCheckoutEventHandler(IMediator mediator, ILogger logger, IMapper mapper)
    {
        _mediator = mediator;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        var command = _mapper.Map<CreateOrderCommand>(context.Message);
        var result =await _mediator.Send(command);
        
        _logger.Information("BasketCheckoutEvent consumed successfully. " +"Oder created with Id: {newOrderId}",result.Data);
    }
}