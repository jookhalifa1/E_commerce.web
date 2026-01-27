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
        #region Include
        public ICollection<Expression<Func<TEntity, object >>> IncludeExpressions { get; } = [];
        public void AddInclude(Expression<Func<TEntity, object>> incldueExp)
        {
            IncludeExpressions.Add(incldueExp);
        }
        #endregion

        #region Order
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public void AddOrderBy (Expression<Func<TEntity,object>> expression)
        {
            OrderBy=expression;
        }

        public Expression<Func<TEntity, object>> OrderByDesc { get; private set; }

        public void AddOrderByDesc(Expression<Func<TEntity, object>> expression)
        {
            OrderByDesc = expression;
        }
        #endregion



        #region Criteria
        public Expression<Func<TEntity, bool>> Criteria { get; }

        protected BaseSpecifications(  Expression<Func<TEntity,bool>> expression )
        {
            Criteria = expression;
        }
        #endregion




    }
}
