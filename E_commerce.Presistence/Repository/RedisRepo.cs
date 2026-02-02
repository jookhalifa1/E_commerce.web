using E_Commerce.Domain.Contract;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Presistence.Repository
{
    public class RedisRepo : IRedisRepo
    {
      private readonly  IDatabase _database;
        public RedisRepo(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }

        public async Task<string?> GetAsync(string Cahchekey)
        {
            var data= await _database.StringGetAsync(Cahchekey);

            return data.IsNullOrEmpty ? null : data.ToString();
        
        }

        public async Task SetAsync(string CacheKey, string CahceValue, TimeSpan time)
        {
           await  _database.StringSetAsync(CacheKey, CahceValue, time);
        }
    }
}
