using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entity.BasketModule
{
     public  class BasketItem
    {
        public string Id { get; set; }
        public string Name { get; set; }= default!;
        public string PictureUrl { get; set; } = default!;
        
        public decimal Price { get; set; } 

        public int Quantity { get; set; }
    }
}
