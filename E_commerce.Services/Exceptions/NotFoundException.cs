using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Services.Exceptions
{
     public abstract class NotFoundException(string massage):Exception(massage)
    {
    }

    public sealed class ProductNotFound(int id):NotFoundException($"Product has id {id} not Found ") { }

    public sealed class BasketNotFound (string id ):NotFoundException($"Basket has id {id} NotFound") { }
}
