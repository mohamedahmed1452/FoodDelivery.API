using FoodDelivery.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FoodDelivery.Core.specifications
{
    public class Specifications<T> : ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>>? Criteria { get; set; } = null;
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>>? OrderBy { get; set; } = null;
        public Expression<Func<T, object>>? OrderByDescending { get; set; } = null;
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPaginationEnabled { get; set; }

        public Specifications(Expression<Func<T, bool>>? criteria)
        {
            Criteria = criteria;
        }

        public void AddOrderBy(Expression<Func<T, object>>? OrderBy)
        {
         this.OrderBy = OrderBy;
        }
        public void AddOrderByDescending(Expression<Func<T, object>>? OrderByDescending)
        {
            this.OrderByDescending = OrderByDescending;
        }

        public void ApplyPagination(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPaginationEnabled = true;
        }


    }
}
