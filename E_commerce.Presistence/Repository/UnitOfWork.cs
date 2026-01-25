using E_commerce.Presistence.Data.DBContexts;
using E_Commerce.Domain.Entity;
using E_Commerce.Domain.GenericRepository;
using E_Commerce.Domain.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Presistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext context;
        private readonly Dictionary<Type, object> _repository = [];
        public UnitOfWork( StoreDbContext context)
        {
            this.context = context;
        }
        public IGenericRepository<TEntity, Tkey> GetRepo<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            var EntityType = typeof(TEntity);
            if(_repository.TryGetValue(EntityType,out object? repo))
            {
                return ((IGenericRepository<TEntity, Tkey>)repo);
            }
            var NewRpeo = new GenericRepository<TEntity, Tkey>(context);
            _repository[EntityType] = NewRpeo;
            return NewRpeo;
        }


        public async Task<int> saveChangeRepository()
        
        =>     await context.SaveChangesAsync();
        
    }
}
