using E_commerce.Sahred.AuthenticationDtos;
using E_commerce.Sahred.CommonResult;
using E_commerce.Services_Abstraction;
using E_Commerce.Domain.Entity.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Services
{
    public class AuthenticationServ : IAuthenticationServ
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public AuthenticationServ( UserManager<ApplicationUser> userManager,IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }
        public async Task<Result<UserDto>> LoginAsync(LoginDto login)
        {
            var user=await userManager.FindByEmailAsync(login.Email);
            if(user is   null)
            {
                return Error.InvalidCredentials("User InvalidCredentials");
            }
            var password = await userManager.CheckPasswordAsync(user,login.Password);
            if (!password)
            {
                return Error.InvalidCredentials("User InvalidCredentials");

            }
            var token =await createTokenAsync(user);
            return new UserDto(user.DisplayName, token, user.Email!);
        }


        public async Task<Result<UserDto>> RegisterAsync(RegisterDto register)
        {
            var user = new ApplicationUser()
            {
                DisplayName = register.DisplayName,
                UserName = register.name,
                Email = register.email,
                PhoneNumber = register.phone,

            };
          var userPassword=  await  userManager.CreateAsync(user, register.password);
            if (userPassword.Succeeded)
            {
                var token = await createTokenAsync(user);
                return new UserDto(user.DisplayName, token, user.Email);
            }
             return userPassword.Errors.Select(e=> Error.Validation(e.Code,e.Description)).ToList();
        }



        private async Task<string> createTokenAsync(ApplicationUser user)
        {
            //Token[Issuer,Audience,Claims,Time]

            string secretKey = configuration["JWTOptions:SecretKey"];
            var key=  new SymmetricSecurityKey( Encoding.UTF8.GetBytes(secretKey));
            

            var cred = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var claim = new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name,user.UserName)
                };

            var roles = await userManager.GetRolesAsync(user);
            foreach (var item in roles)
            {
                var Claims = new Claim(ClaimTypes.Role, item);
                claim.Add(Claims);
            }

            var token = new JwtSecurityToken(
                    issuer: configuration["JWTOptions:Issuer"],
                    audience: configuration["JWTOptions:Audience"],
                    claims:claim,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: cred
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

             
        } 
    }
}
