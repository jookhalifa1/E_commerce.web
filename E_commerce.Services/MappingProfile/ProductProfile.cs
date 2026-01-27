using AutoMapper;
using E_commerce.Sahred;
using E_Commerce.Domain.Entity.ProductEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Services.MappingProfile
{
     public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductBrand, GetBrand>();
            CreateMap<Product, ProductDto>().ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.productBrand.Name)).
                                             ForMember(dest=>dest.TypeId,opt=>opt.MapFrom(src=>src.productType.Name))
                                             .ForMember(dest=>dest.PictureUrl,opt=>opt.MapFrom<ProductPictureUrlResolver>());
                                                    
            CreateMap<ProductType, GetType>();
        }
    }
}
