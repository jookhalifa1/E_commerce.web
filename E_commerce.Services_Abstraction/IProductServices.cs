using E_commerce.Sahred;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Services_Abstraction
{
     public interface IProductServices
    {
        Task<PaginatedResult<  ProductDto >> GetALLProductsASync( QueryParams queryParams );

        Task<ProductDto> GetProductByIdAsync(int id);

        Task<IEnumerable<GetBrand>> GetAllBrandsAsync();
        Task<IEnumerable<GetType>> GetAllTypeAsync();


    }
}
