using E_commerce.Sahred.CommonResult;
using E_commerce.Sahred.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Services_Abstraction
{
     public interface IOrderServices
    {
        public Task<Result<OrderToReturnDto>> CreateOrderAsync(OrderDto order, string Email);
        public Task< Result< IEnumerable<OrderToReturnDto>>>  GetAllOrdersAsync(string UserEmail);

        public Task<Result<OrderToReturnDto>> GetOrderByIdAsync(Guid id);

        public Task<Result<IEnumerable<DeliveryMethodDto>>> GetAllDeliveriesAsync();
        
    }
}
