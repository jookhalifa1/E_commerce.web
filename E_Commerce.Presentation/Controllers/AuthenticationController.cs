using E_commerce.Sahred.AuthenticationDtos;
using E_commerce.Sahred.CommonResult;
using E_commerce.Services_Abstraction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
     public class AuthenticationController:ApiControllerBase
    {
        private readonly IAuthenticationServ authentication;

        public AuthenticationController( IAuthenticationServ authentication)
        {
            this.authentication = authentication;
        }


        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto login)
        {
            var result = await authentication.LoginAsync(login);
            
                return HandelRequest(result);
             
             
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto register)
        {
            var result=await authentication.RegisterAsync(register);
            return HandelRequest(result);
        }
    }
}
