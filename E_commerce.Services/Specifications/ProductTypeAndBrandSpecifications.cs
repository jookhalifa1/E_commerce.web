using E_commerce.Sahred;
using E_Commerce.Domain.Entity.ProductEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization.Metadata;
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
        public ProductTypeAndBrandSpecifications(QueryParams queryParams) : base(p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId.Value)
                                                                                && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId.Value)
                                                                                && (string.IsNullOrEmpty(queryParams.Search) || p.Name.ToLower().Contains(queryParams.Search.ToLower()) )   )
        {

            
            AddInclude(p => p.productType );
            AddInclude(p => p.productBrand );

            switch (queryParams.sorted)
            {
                case  SortedParams.NameAsc:
                    AddOrderBy(p => p.Name);
                        break;
                case SortedParams.NameDesc:
                    AddOrderByDesc(p => p.Name); break;
                case SortedParams.PriceAsc:
                    AddOrderBy(p => p.Price); break;
                case SortedParams.PriceDesc:
                    AddOrderByDesc(p => p.Price); break;
                default:
                    AddOrderBy(p => p.Id); break;
            }


        }
    }
}
