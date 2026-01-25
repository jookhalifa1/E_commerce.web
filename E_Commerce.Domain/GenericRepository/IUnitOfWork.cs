using E_Commerce.Domain.Entity;
using E_Commerce.Domain.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.GenericRepository
{
    public interface IUnitOfWork
    {

        IGenericRepository<TEntity, Tkey> GetRepo<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>;

        Task<int> saveChangeRepository();
    }
}
