using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Sahre
{
     public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        public  string BrandId { get; set; }= default!;


        public string TypeId { get; set; }=default!;
    }
}
