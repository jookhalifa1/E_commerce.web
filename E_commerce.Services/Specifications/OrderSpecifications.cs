using E_Commerce.Domain.Entity.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Services.Specifications
{
    public class OrderSpecifications:BaseSpecifications<Order,Guid>
    {
        public OrderSpecifications(string userEmail):base(u=>u.UserEmail==userEmail)
        {

        
            AddInclude(x => x.deliveryMethod);
            AddInclude(x => x.items);

        }
        public OrderSpecifications(Guid id )  :base(x=>x.Id==id)
        {


            AddInclude(x => x.deliveryMethod);
            AddInclude(x => x.items);
             
        }

    }
}
