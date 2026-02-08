using E_commerce.Sahred;
using E_commerce.Services_Abstraction;
using E_Commerce.Presentation.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
   
    public class ProductController :  ApiControllerBase
    {
        private readonly IProductServices services;

        public ProductController(IProductServices services)
        {
            this.services = services;
        }
        [Authorize(Roles ="Admin")]
        [HttpGet]
        [RedisCache]
        public async Task<ActionResult<PaginatedResult< ProductDto >>> GetAllProduct([FromQuery] QueryParams Params )
        {
           
            var products = await services.GetALLProductsASync( Params );
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById( int id)
        {
            
                var product = await services.GetProductByIdAsync(id);
            return HandelRequest<ProductDto>(product);  
             
            
                   
        }
        [HttpGet("Types")]

        public async Task<ActionResult<IEnumerable<GetType>>> GetAllTypes()
        {
            var types = await services.GetAllTypeAsync();
            return Ok(types);
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<GetBrand>>> GetAllBrands()
        {
            var brands = await services.GetAllBrandsAsync();
            return Ok(brands);
        }
    }
}
