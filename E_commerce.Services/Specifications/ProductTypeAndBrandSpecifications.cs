using E_Commerce.Domain.Entity.ProductEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Services.Specifications
{
     public class ProductTypeAndBrandSpecifications:BaseSpecifications<Product,int>
    {
        public ProductTypeAndBrandSpecifications( int id ):base(p=>p.Id==id)
        {

            AddInclude(p => p.productType);
            AddInclude(p => p.productBrand);

        }
        public ProductTypeAndBrandSpecifications():base(null)
        {
            AddInclude(p => p.productType );
            AddInclude(p => p.productBrand );

        }
    }
}
