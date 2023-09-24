using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Common.Models;
using Ordering.Application.Features.V1.Orders.Queries.GetOrders;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Ordrering.API.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OrdersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper;
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
    }
}
