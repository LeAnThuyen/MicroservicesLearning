using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Common.Models;
using Ordering.Application.Features.V1.Orders.Queries.GetOrders;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Contracts.Messages;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Services;
using Shared.Services.Email;
using Swashbuckle.AspNetCore.Annotations;

namespace Ordrering.API.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ISmtpEmailService _emailService;
        private readonly IMessageProducer _messageProducer;
        private readonly IOrderRepository _orderRepository;

       
        public OrdersController(IMediator mediator, IMapper mapper, ISmtpEmailService emailService, IMessageProducer messageProducer, IOrderRepository orderRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper;
            _emailService = emailService;
            _messageProducer = messageProducer ?? throw new ArgumentNullException(nameof(messageProducer));
            _orderRepository = orderRepository;
        }


        private static class RouteNames
        {
            public const string GetOrders = nameof(GetOrders);
        }
        [HttpGet("{userName}", Name = RouteNames.GetOrders)]
        [ProducesResponseType(typeof(IEnumerable<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrderByUserNameAsync([Required] string userName)
        {
            var query = new GetOrdersQuery(userName);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> TestMail()
        {
            var message = new MailRequest
            {
                Body = "<h2>hello world</h2>",
                Subject = "Tana when the king is back!",
                ToAddress = "leanthuyen08122002@gmail.com"

            };
            await _emailService.SendEmailAsync(message);
            return Ok();
        }
        
        [HttpPost]
        [SwaggerOperation(Summary = "RabbitMQ by An Thuyen Le")]
        public async Task<IActionResult> CreateOrder(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            var addedOrder = await _orderRepository.CreateOrder(order);
            await _orderRepository.SaveChangesAsync();
    
            var result = _mapper.Map<OrderDto>(addedOrder);
            _messageProducer.SendMessages(result);
            return Ok(result);
        }
    }
}
