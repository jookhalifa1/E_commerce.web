using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entity.OrderModule
{
     public class Order:BaseEntity<Guid>
    {
        public string UserEmail { get; set; } = default!;

        public DateTimeOffset orderDate { get; set; }=DateTimeOffset.Now;

        public OrderStatues  statues { get; set; }=OrderStatues.Pending;

        public ShippingAddress Address { get; set; } = default!;
        public DeliveryMethod deliveryMethod { get; set; }=default!;

        public int deliveryMethodID { get; set; }
        public ICollection<ItemsOfOrder> items { get; set; } = [];

        public decimal SubTotal { get; set; }

        public decimal GetTotal()=>SubTotal+deliveryMethod.Price;
    }
}
