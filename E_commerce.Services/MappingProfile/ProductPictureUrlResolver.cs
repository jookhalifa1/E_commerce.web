using AutoMapper;
using E_commerce.Sahre;
using E_Commerce.Domain.Entity.ProductEntity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Services.MappingProfile
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
                return string.Empty;
            if (source.PictureUrl.StartsWith("http"))
                return string.Empty;

            var BaseUrl = configuration.GetSection("Urls")["BaseUrl"];
            if (string.IsNullOrEmpty(BaseUrl))
                return string.Empty;

            var picture = $"{BaseUrl}/{source.PictureUrl}";

            return picture;

        }
    }
    

}
