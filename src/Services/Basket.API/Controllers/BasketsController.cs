using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.ComponentModel.DataAnnotations;
using System.Net;
using AutoMapper;
using EventBus.Messages.IntegrationEvents.Events;
using MassTransit;
using Swashbuckle.AspNetCore.Annotations;

namespace Basket.API.Controllers
{

    [ApiController]
    [Route("api/[Controller]")]
    public class BasketsController : ControllerBase
    {

        private readonly IBasketRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;
        public BasketsController(IBasketRepository repository, IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            _repository = repository;
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
        }


        [HttpGet("{userName}")]
        [ProducesResponseType(typeof(Cart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetBasketByUserName([Required] string userName)
        {
            var result = await this._repository.GetBasketByUsername(userName);
            return Ok(result ?? new Cart());
        }

        [HttpPost(Name = "UpdateBasket")]
        public async Task<IActionResult> UpdateBasket([FromBody] Cart cart)
        {
            var option = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.UtcNow.AddDays(1)).SetSlidingExpiration(TimeSpan.FromMinutes(5));

            var result = await _repository.UpdateBasket(cart, option);
            return Ok(result);
        }

        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteBaseket([Required] string userName)
        {
            var result = await _repository.DeleteBasketFromUsername(userName);
            return result;
        }
        
        [Route("[action]")]
        [HttpPost]
        [SwaggerOperation(Summary = "Masstransit and RabbitMQ Integrated By An Thuyen Le Fucking Dep Trai")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Accepted)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            var basket = await _repository.GetBasketByUsername(basketCheckout.UserName);
            if (basket is null) return NotFound();
            
            //publish Checkout event to EventBus message
            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.TotalPrice = basket.TotalPrice;
            _publishEndpoint.Publish(eventMessage);
            // remove the user basket
         //   await _repository.DeleteBasketFromUsername(basketCheckout.UserName);
            return Accepted();

        }
    }
}
