using AutoMapper;
using E_commerce.Sahred.OrderDtos;
using E_Commerce.Domain.Entity.OrderModule;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace E_commerce.Services.MappingProfile
{
    public class OrderPictureUrlResolver : IValueResolver<ItemsOfOrder, OrderItemDto, string>
    {
        private readonly IConfiguration configuration;

        public OrderPictureUrlResolver( IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string Resolve(ItemsOfOrder source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.productItemOrder.PictureUrl))
            {
                return string .Empty;
            
            }
            if (source.productItemOrder.PictureUrl.StartsWith("http")) return source.productItemOrder.PictureUrl;
            var BaseUrl = configuration.GetSection("Urls")["BaseUrl"];
            if (string.IsNullOrEmpty(BaseUrl)) return string.Empty;

            var order = $"{BaseUrl}/{source.productItemOrder.PictureUrl}";
            return order;


        }
    }
}