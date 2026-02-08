using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Sahred.AuthenticationDtos
{
     public record RegisterDto([EmailAddress]string email,string name ,[Phone]string phone,string password,string DisplayName);
     
}
