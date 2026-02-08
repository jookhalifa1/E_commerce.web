using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Sahred.AuthenticationDtos
{
     public record LoginDto([EmailAddress]string Email,string Password);
     
}
