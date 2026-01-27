using E_commerce.Sahred;
using E_commerce.Services.Specifications.PrdouctSpecification;
using E_Commerce.Domain.Entity.ProductEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Services.Specifications.ProductSpecification
{
    public class CountElementProductSpecification : BaseSpecifications<Product, int>
    {
        public CountElementProductSpecification(QueryParams queryParams) : base(ProductSpecificationHelper.GetExpression(queryParams))
            {
            
        }
    }
}
