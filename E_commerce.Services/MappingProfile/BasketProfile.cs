using AutoMapper;
using E_commerce.Sahred.BasketDtos;
using E_Commerce.Domain.Entity.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Services.MappingProfile
{
    public class BasketProfile:Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketDto, CustomerBasket>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
           
        }

    }
}
