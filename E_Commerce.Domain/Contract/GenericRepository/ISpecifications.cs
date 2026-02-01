using E_Commerce.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contract.GenericRepository
{
     public interface ISpecifications<TEntity,Tkey> where TEntity : BaseEntity<Tkey> 
    {
        public   ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; }

        public Expression<Func<TEntity,bool>> Criteria { get; }

        public Expression<Func<TEntity,object>> OrderBy { get; }
        public Expression<Func<TEntity, object>> OrderByDesc { get; }

        public int Take { get;  }
        public int Skip { get; }
        

        public bool IsPaginated { get; }


    }
}
