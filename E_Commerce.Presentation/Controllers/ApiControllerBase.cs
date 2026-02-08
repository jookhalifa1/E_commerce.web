using E_commerce.Sahred.CommonResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiControllerBase:ControllerBase
    {


        //Function Return Result
        
         protected IActionResult HandelRequest(Result result)
        {
            if (result.IsSuccess)
            {
                return NoContent();//204
            }
            else
            {
                return HandelProblem(result.errors);
            }
        }

        //Function Return Result<Value>
       
         protected ActionResult<Tvalue> HandelRequest<Tvalue>(Result<Tvalue> result)
        {
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return HandelProblem(result.errors);
            }
        }
        //Function Return Error TO Handel the void result and Result<Value>
        //
         protected ActionResult HandelProblem(IReadOnlyList<Error> errors)
        {
            if (errors.Count == 0)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError, title: "An Unexpected Error Occurred ");
            }

            if (errors.All(E => E.Type == ErrorType.Validation))
                return HandelValidationProblem(errors);
            return HandelSignelError(errors[0]);

        }
       private ActionResult HandelSignelError(Error error)
        {
              return Problem(
                  title:error.Code,
                  detail:error.Description,
                  type:error.Type.ToString(),
                  statusCode:MapErrorTypeToStatuesCode(error.Type)
            );
             
        }

         private static int MapErrorTypeToStatuesCode(ErrorType type) => type switch
        {
            ErrorType.NotFound=>StatusCodes.Status404NotFound,
            ErrorType.Validation=>StatusCodes.Status400BadRequest,
            ErrorType.Forbidden=>StatusCodes.Status401Unauthorized,
            ErrorType.InvalidCredentials=>StatusCodes.Status401Unauthorized,
            ErrorType.Failure=>StatusCodes.Status500InternalServerError,
            _=> StatusCodes.Status500InternalServerError

        };

        private ActionResult HandelValidationProblem(IReadOnlyList<Error> errors)
        {
            var Model = new ModelStateDictionary();
            foreach (var error in errors)
            {
                ModelState.AddModelError(error.Code,error.Description);

            }
            return ValidationProblem();
        }
    }
}
