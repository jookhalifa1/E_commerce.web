using E_Commerce.Domain.Contract;
using E_Commerce.Domain.Entity.BasketModule;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_commerce.Presistence.Repository
{
    public class BasketRepository : IBasketRepository
    {

        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer connection)
        {
            _database=connection.GetDatabase();

        }

        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan time = default)
        {
            var jsonData = JsonSerializer.Serialize(basket);
            var BasketDataCreatedOrUpdated = await _database.StringSetAsync(basket.Id, jsonData, (time == default) ? TimeSpan.FromDays(7) : time);
            if (BasketDataCreatedOrUpdated)
            {
                return await GetBasketByIdAsync(basket.Id);
            }
            else
            {
               return null
            }
        }


        public async Task<CustomerBasket?> GetBasketByIdAsync(string BasketId)
        {
           var JsonData=await _database.StringGetAsync(BasketId);

            var Data = JsonSerializer.Deserialize<CustomerBasket>(JsonData)
               return(Data);
        }
        
       

        public async Task<bool> DeleteBasketAsync(string BasketId)
        {
            return await _database.KeyDeleteAsync(BasketId);
        }

    }
}
