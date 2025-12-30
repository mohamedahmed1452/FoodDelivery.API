using FoodDelivery.Core.Entities;
using FoodDelivery.Core.specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodDelivery.Infrastructure
{
    internal static class SpecificationsEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> entryQuery,ISpecifications<T> spec)
        {
            //TO DO: later we can add more logic here if needed
            #region Filteration
            if (spec.Criteria != null)
                entryQuery = entryQuery.Where(spec.Criteria);
            #endregion

            #region Sorting asc|desc
            if (spec.OrderBy != null)
            {
                entryQuery = entryQuery.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderByDescending != null)
            {
                entryQuery = entryQuery.OrderByDescending(spec.OrderByDescending);
            }
            #endregion




            #region Pagination

            if (spec.IsPaginationEnabled)
            {
                entryQuery = entryQuery.Skip(spec.Skip).Take(spec.Take);
            }
            #endregion


            #region Inner Join => eager loading
            var query = spec.Includes.Aggregate(entryQuery, (first, second) => first.Include(second));

            #endregion


            return query;
        }
    }
}
