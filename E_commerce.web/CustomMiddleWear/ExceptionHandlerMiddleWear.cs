using E_commerce.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.web.CustomMiddleWear
{
    public class ExceptionHandlerMiddleWear
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlerMiddleWear> logger;

        public ExceptionHandlerMiddleWear(RequestDelegate next, ILogger<ExceptionHandlerMiddleWear> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);

                await NotFoundEndPointAsync(context);
            }
            catch (Exception ex)
            {
               

                logger.LogError(ex, "Something Error");


                var problem = new ProblemDetails()
                {
                    Title = "Error While Processing Http Request",
                    Detail= ex.Message,
                    Instance = context.Request.Path,

                     Status = ex switch {
                        NotFoundException=>StatusCodes.Status404NotFound,
                        _=>StatusCodes.Status500InternalServerError
                     }


                };
                context.Response.StatusCode = problem.Status.Value;
                await context.Response.WriteAsJsonAsync(problem);



            }
        }

        private static async Task NotFoundEndPointAsync(HttpContext context)
        {
            if (context.Response.StatusCode == StatusCodes.Status404NotFound && !context.Response.HasStarted)
            {
                var problem = new ProblemDetails()
                {
                    Title = " Not Found End Point",
                    Detail = $" {context.Request.Path} not found",
                    Status = StatusCodes.Status404NotFound,
                    Instance = $" {context.Request.Path} not found"
                };
                await context.Response.WriteAsJsonAsync(problem);

            }
        }
    }
}
