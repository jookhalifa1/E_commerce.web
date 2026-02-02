using E_commerce.Services_Abstraction;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Attributes
{
     public class RedisCacheAttribute:ActionFilterAttribute
    {
        private readonly int time;

        public RedisCacheAttribute( int time =5)
        {
            this.time = time;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var Services = context.HttpContext.RequestServices.GetRequiredService<IRedisServices>();
            //cacheKey=>Path

            var key = createCacheKey(context.HttpContext.Request);

            var data = await Services.GetAsync(key);
            if(data  is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = data,
                    ContentType = "application/Json",
                    StatusCode = StatusCodes.Status200OK,

                };
                return;
            }
            
            
                var ExecutedDate = await  next.Invoke();

                 if(ExecutedDate.Result is OkObjectResult result)
            {
              await  Services.SetAsync(key, result.Value, TimeSpan.FromMinutes(time));
            }
            
            


        }



      private string createCacheKey ( HttpRequest request )
        {
             StringBuilder Path=new StringBuilder();
            //api/controller
            Path.Append(request.Path);


            foreach( var item in request.Query.OrderBy(x=>x.Key))
            {
                Path.Append($"{item.Key}-{item.Value}");
            }
            return Path.ToString();

        }
    }
}
