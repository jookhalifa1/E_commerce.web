using E_commerce.Sahred.BasketDtos;
using E_commerce.Services_Abstraction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
     public class BasketController:ControllerBase
    {
        private readonly IBasketServcies basketServcies;

        public BasketController( IBasketServcies basketServcies)
        {
            this.basketServcies = basketServcies;
        }
        [HttpGet]

        public async Task<ActionResult<BasketDto>> Get( string id)
        {
            var data=await basketServcies.GetBasketByIdAsync(id);

            return Ok(data);

        }
        [HttpPost]

        public async Task<ActionResult<BasketDto>> CreateORUpdate( BasketDto basket)
        {
            var bb = await basketServcies.CreateOrUpdateBasketAsync(basket);
            return Ok(bb);
        }


        [HttpDelete]


        public async Task<ActionResult<bool>> DeleteBasket( string id)
        {
            var result=await basketServcies.DeleteBasketAsync(id);
            return Ok(result);
        }




    }
}
