using E_commerce.Sahred.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Services_Abstraction
{
     public interface IBasketServcies
    {
        public Task<BasketDto> GetBasketByIdAsync(string Id);

        public Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket);

        public Task<bool> DeleteBasketAsync (string Id);

    }
}
