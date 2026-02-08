using E_commerce.Sahred.AuthenticationDtos;
using E_commerce.Sahred.CommonResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Services_Abstraction
{
     public interface IAuthenticationServ
    {
        public Task<Result<UserDto>> LoginAsync(LoginDto login);

        public Task<Result<UserDto>> RegisterAsync(RegisterDto register);
    }
}
