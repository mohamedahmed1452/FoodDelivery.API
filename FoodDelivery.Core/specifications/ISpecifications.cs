using FoodDelivery.Core.Entities;
using System.Linq.Expressions;

namespace FoodDelivery.Core.specifications
{
    public interface ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>>? Criteria { get; set; }// for where clause
        public List<Expression<Func<T, object>>> Includes { get; set; }// for include navigation properties

        public Expression<Func<T, object>>? OrderBy { get; set; }// for sorting by ascending
        public Expression<Func<T, object>>? OrderByDescending { get; set; }// for sorting by descending

        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPaginationEnabled { get; set; }


    }
}
