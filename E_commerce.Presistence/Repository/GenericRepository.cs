using E_commerce.Presistence.Data.DBContexts;
using E_Commerce.Domain.Entity;
using E_Commerce.Domain.GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Presistence.Repository
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext context;

        public GenericRepository( StoreDbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(TEntity entity)
        
        =>    await context.Set<TEntity>().AddAsync(entity);

        public async Task<int> CountElementAsync(ISpecifications<TEntity, TKey> specifications)
        {
            var ans = SpecificationsFactory.CreateQuery(context.Set<TEntity>(), specifications);
            return await ans.CountAsync();

        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        { 
         return     await context.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications)
        {
            IQueryable<TEntity> Query = context.Set<TEntity>();
           var ans= SpecificationsFactory.CreateQuery(Query, specifications);
            return await ans.ToListAsync();

        }


        public async Task<TEntity?> GetById(TKey id)
        
            =>await context.Set<TEntity>().FindAsync(id);

        public async Task<TEntity> GetById(ISpecifications<TEntity, TKey> specifications)
        {
            var Query = SpecificationsFactory.CreateQuery(context.Set<TEntity>(), specifications);
            return await Query.FirstOrDefaultAsync();
        }


        public void remove(TEntity entity)
        
            =>context.Set<TEntity>().Remove(entity);
        

        public void update(TEntity entity)
        
           => context.Set<TEntity>().Update(entity);
        
    }
}
