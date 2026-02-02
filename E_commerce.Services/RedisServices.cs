using E_commerce.Services_Abstraction;
using E_Commerce.Domain.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_commerce.Services
{
    public class RedisServices : IRedisServices
    {
        private readonly IRedisRepo repo;

        public RedisServices( IRedisRepo repo)
        {
            this.repo = repo;
        }
        public Task<string> GetAsync(string cacheKey)
        {
            return repo.GetAsync(cacheKey);
        }

        public Task SetAsync(string cacheKey, object cacheValue, TimeSpan time)
        {
            var data=JsonSerializer.Serialize(cacheValue);
            return repo.SetAsync(cacheKey, data, time);

        }
    }
}
