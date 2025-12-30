using FoodDelivery.Core.Entities;
using FoodDelivery.Core.specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Core.Repositories
{
    public interface IGenericRepository<T> where T:BaseEntity
    {
        //five signatures
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetAsync(int id);
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec);
        Task<T?> GetWithSpecAsync(ISpecifications<T> spec);
        Task<int> CountAsync(ISpecifications<T> spec);


    }
}
