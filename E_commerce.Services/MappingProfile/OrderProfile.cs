using AutoMapper;
using E_commerce.Sahred.OrderDtos;
using E_Commerce.Domain.Entity.OrderModule;
using Microsoft.AspNetCore.Routing.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace E_commerce.Services.MappingProfile { 
    public class OrderProfile : Profile {
        public OrderProfile() 
        { 
            CreateMap<Order, OrderToReturnDto>().ForMember(d => d.DeliveryMethod, o => o.MapFrom(src => src.deliveryMethod.ShortName));
            CreateMap<ShippingAddress, AddressDto>().ReverseMap();
            CreateMap<ItemsOfOrder, OrderItemDto>()
     .ForMember(d => d.ProductName, o => o.MapFrom(src => src.productItemOrder != null ? src.productItemOrder.Name : ""))
     .ForMember(d => d.PictureUrl, o => o.MapFrom(src => src.productItemOrder != null ? src.productItemOrder.PictureUrl : ""))
     .ForMember(d => d.Price, o => o.MapFrom(src => src.Price))
     .ForMember(d => d.Quantity, o => o.MapFrom(src => src.Quantity));


            CreateMap<DeliveryMethod, DeliveryMethodDto>();
        }
    } }