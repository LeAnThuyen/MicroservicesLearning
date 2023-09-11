using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Basket.API.Controllers
{

    [ApiController]
    [Route("api/[Controller]")]
    public class BasketsController : ControllerBase
    {

        private readonly IBasketRepository _repository;
        public BasketsController(IBasketRepository repository)
        {
            _repository = repository;
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
    }
}
