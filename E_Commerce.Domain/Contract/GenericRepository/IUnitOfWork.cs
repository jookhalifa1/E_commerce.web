using E_Commerce.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contract.GenericRepository
{
    public interface IUnitOfWork
    {

        IGenericRepository<TEntity, Tkey> GetRepo<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>;

        Task<int> saveChangeRepository();
    }
}
