using AutoMapper;
using AutoMapper.Configuration.Annotations;
using E_commerce.Sahre;
using E_commerce.Services.Specifications;
using E_commerce.Services_Abstraction;
using E_Commerce.Domain.Entity.ProductEntity;
using E_Commerce.Domain.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductServices(IUnitOfWork unitOfWork,IMapper mapper )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<ProductDto>> GetALLProductsASync()
        {
            var spec =  new ProductTypeAndBrandSpecifications();

            var products=await unitOfWork.GetRepo<Product,int>().GetAllAsync(spec);

            return mapper.Map<IEnumerable<ProductDto>>(products);

        }
        public async Task<IEnumerable<GetBrand>> GetAllBrandsAsync()
        {
            var brands = await unitOfWork.GetRepo<ProductBrand, int>().GetAllAsync();
            var brandsdto = mapper.Map<IEnumerable<GetBrand>>(brands);
            return brandsdto;
            
        }


   

        public async Task<IEnumerable<GetType>> GetAllTypeAsync()
        {
            
            return mapper.Map<IEnumerable<GetType>>( await unitOfWork.GetRepo<ProductType, int>().GetAllAsync());
             
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var spec=new ProductTypeAndBrandSpecifications(id);
            return mapper.Map<ProductDto>(await unitOfWork.GetRepo<Product, int>().GetById(spec)); 
        }
    }
}
