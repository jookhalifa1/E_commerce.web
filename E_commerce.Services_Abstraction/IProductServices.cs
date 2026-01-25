using E_commerce.Sahre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Services_Abstraction
{
     public interface IProductServices
    {
        Task<IEnumerable<ProductDto>> GetALLProductsASync();

        Task<ProductDto> GetProductByIdAsync(int id);

        Task<IEnumerable<GetBrand>> GetAllBrandsAsync();
        Task<IEnumerable<GetType>> GetAllTypeAsync();


    }
}
