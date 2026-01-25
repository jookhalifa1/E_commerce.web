using E_Commerce.Domain.Entity;
using E_Commerce.Domain.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Services.Specifications
{
    public abstract class BaseSpecifications<TEntity, Tkey> : ISpecifications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public ICollection<Expression<Func<TEntity, object >>> IncludeExpressions { get; } = [];

        public Expression<Func<TEntity, bool>> Criteria { get; }

        public void AddInclude(Expression<Func<TEntity,object>> incldueExp)
        {
            IncludeExpressions.Add(incldueExp);
        }

        protected BaseSpecifications(  Expression<Func<TEntity,bool>> expression )
        {
            Criteria = expression;
        }




    }
}
