using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Sahred.OrderDtos
{
    public class OrderToReturnDto
    {
        public Guid OrderId { get; set; }
        public string UserEmail { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; }
        public AddressDto Address { get; set; }
        public string DeliveryMethod { get; set; }
        public string OrderStatues { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
    }


}
