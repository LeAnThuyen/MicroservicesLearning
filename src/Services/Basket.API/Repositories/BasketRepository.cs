using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Contracts.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using ILogger = Serilog.ILogger;
namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCacheService;
        private readonly ISerializeService _serializeService;
        private readonly ILogger _logger;

        public BasketRepository(IDistributedCache redisCacheService, ISerializeService serializeService, ILogger logger)
        {
            _redisCacheService = redisCacheService;
            _serializeService = serializeService;
            _logger = logger;
        }

        public async Task<bool> DeleteBasketFromUsername(string username)
        {
            try
            {
                _logger.Information($"Begin : DeleteBasketFromUsername {username}");
                var result = _redisCacheService.RemoveAsync(username);
                _logger.Information($"End : DeleteBasketFromUsername {username}");
                return true;
            }
            catch (Exception e)
            {

                _logger.Error("DeleteBasketFromUsername" + e.Message);
                throw;
            }


        }

        public async Task<Cart?> GetBasketByUsername(string username)
        {
            _logger.Information($"Begin : GetBasketByUsername {username}");
            var basket = await _redisCacheService.GetStringAsync(username);
            _logger.Information($"End : GetBasketByUsername {username}");
            return String.IsNullOrEmpty(basket) ? null : _serializeService.Deserialize<Cart>(basket);

        }

        public async Task<Cart> UpdateBasket(Cart cart, DistributedCacheEntryOptions options = null)
        {
            if (options != null)
            {
                _logger.Information($"Begin : UpdateBasket {cart.UserName}");
                await _redisCacheService.SetStringAsync(cart.UserName, _serializeService.Serialize(cart), options);
                _logger.Information($"End : UpdateBasket {cart.UserName}");
            }
            else
            {
                _logger.Information($"Begin : UpdateBasket {cart.UserName}");
                await _redisCacheService.SetStringAsync(cart.UserName, _serializeService.Serialize(cart));
                _logger.Information($"End : UpdateBasket {cart.UserName}");
            }
            return await GetBasketByUsername(cart.UserName);
        }
    }
}
