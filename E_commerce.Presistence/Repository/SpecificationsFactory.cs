using E_Commerce.Domain.Contract.GenericRepository;
using E_Commerce.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace E_commerce.Presistence.Repository
{
    public  class SpecificationsFactory
    {
        // create Query 

       public static IQueryable<TEntity> CreateQuery<TEntity, Tkey>(IQueryable<TEntity> BaseQuery,ISpecifications<TEntity,Tkey>  specifications) where TEntity : BaseEntity<Tkey>
        {
            if (specifications != null)
            {
                if(specifications.Criteria != null)
                {
                    BaseQuery=BaseQuery.Where(specifications.Criteria);
                }
                if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Any())
                {
                    //foreach (var expression in specifications.IncludeExpressions)
                    //{
                    //     BaseQuery = BaseQuery.Include(expression);
                    //}
                    BaseQuery = specifications.IncludeExpressions.Aggregate(BaseQuery, (curentQuery, Incldueexp) => curentQuery.Include(Incldueexp));
                }

                if(specifications.OrderBy != null)
                {
                    BaseQuery=BaseQuery.OrderBy(specifications.OrderBy);
                }
                if(specifications.OrderByDesc != null)
                {
                    BaseQuery=BaseQuery.OrderByDescending(specifications.OrderByDesc);
                }

                if (specifications.IsPaginated)
                {
                    BaseQuery = BaseQuery.Skip(specifications.Skip).Take(specifications.Take);
                }
            }
            return BaseQuery;

        } 

    }
}
