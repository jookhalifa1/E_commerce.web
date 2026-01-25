using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entity.ProductEntity
{
     public class Product:BaseEntity<int>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; }=default!;
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        public   int    BrandId { get; set; }
        public ProductBrand productBrand { get; set; } = default!;

        public  int TypeId { get; set; }
        public ProductType productType { get; set; } = default!;
    }
}
