using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Sahred.BasketDtos
{
    
    public record BasketDto (string Id, ICollection<BasketItemDto> Items)
    {
    }
}
