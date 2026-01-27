using E_Commerce.Domain.Entity;
using E_Commerce.Domain.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Services
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

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginated { get; private set; }
        //50
        //10 10 10 10 10
        //3
        public void Pagination(int PageSize,int PageIndex)
        {
            IsPaginated=true;
            Take=PageSize;
            Skip=(PageIndex-1) * PageSize;

        }




    }
}
