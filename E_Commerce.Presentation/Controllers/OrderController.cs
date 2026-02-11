using E_commerce.Sahred.CommonResult;
using E_commerce.Sahred.OrderDtos;
using E_commerce.Services_Abstraction;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    public class OrderController : ApiControllerBase
    {
        private readonly IOrderServices services;


        public OrderController(IOrderServices services)
        {
            this.services = services;
        }
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto order)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await services.CreateOrderAsync(order, email!);
            return HandelRequest(result);

        }
        [HttpGet]

        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAll()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var result = await services.GetAllOrdersAsync(email);
            return HandelRequest(result);
        }
        [HttpGet("{id}")]
       

        public async Task<ActionResult<OrderToReturnDto>> GetById(Guid id)
        {
          
            return HandelRequest(await services.GetOrderByIdAsync(id));
        }

        [HttpGet("DeliveryMethods")]
        
        public async Task<ActionResult< IEnumerable< DeliveryMethodDto>>> GetAllDelivery()
        {
            return HandelRequest(await services.GetAllDeliveriesAsync());
        }

    }
}
