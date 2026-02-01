using E_Commerce.Domain.Entity.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contract
{
     public interface IBasketRepository
    {
        public Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket,TimeSpan time=default);



        public Task<bool> DeleteBasketAsync(string BasketId );

        public Task<CustomerBasket?> GetBasketByIdAsync(string BasketId ); 

    }
}
