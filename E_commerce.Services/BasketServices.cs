using AutoMapper;
using E_commerce.Sahred.BasketDtos;
using E_commerce.Services.Exceptions;
using E_commerce.Services_Abstraction;
using E_Commerce.Domain.Contract;
using E_Commerce.Domain.Entity.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Services
{
    public class BasketServices : IBasketServcies
    {
        private readonly IBasketRepository repository;
        private readonly IMapper mapper;

        public BasketServices( IBasketRepository repository,IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var Basket = mapper.Map<BasketDto,CustomerBasket>(basket);

               var ans= await repository.CreateOrUpdateBasketAsync(Basket);
            return mapper.Map<CustomerBasket,BasketDto> (ans!);
        }

        public Task<bool> DeleteBasketAsync(string Id)
        {
            return repository.DeleteBasketAsync(Id);
        }

        public async Task<BasketDto> GetBasketByIdAsync(string Id)
        {
            var basket= await repository.GetBasketByIdAsync(Id);
            if(basket is null)
            {
                throw new BasketNotFound(Id);
            }

          return  mapper.Map<CustomerBasket, BasketDto>(basket!);
        }
    }
}
