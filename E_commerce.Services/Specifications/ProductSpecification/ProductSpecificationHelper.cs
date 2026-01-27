using E_commerce.Sahred;
using E_Commerce.Domain.Entity.ProductEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Services.Specifications.PrdouctSpecification
{
     public static  class ProductSpecificationHelper
    {   public static Expression<Func<Product, bool>> GetExpression(   QueryParams queryParams )
        {
            return
        p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId.Value)
             && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId.Value)
          && (string.IsNullOrEmpty(queryParams.Search) || p.Name.ToLower().Contains(queryParams.Search.ToLower()));
     

        }

    }
}
