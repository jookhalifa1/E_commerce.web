using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Services_Abstraction
{
     public interface IRedisServices
    {
        public Task<string> GetAsync(string cacheKey);


        public Task SetAsync(string cacheKey, object cacheValue, TimeSpan time);

    }
}
