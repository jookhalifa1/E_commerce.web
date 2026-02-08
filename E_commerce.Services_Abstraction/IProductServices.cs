using E_commerce.Sahred;
using E_commerce.Sahred.CommonResult;
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

        Task<Result< ProductDto>> GetProductByIdAsync(int id);

        Task<IEnumerable<GetBrand>> GetAllBrandsAsync();
        Task<IEnumerable<GetType>> GetAllTypeAsync();


    }
}
