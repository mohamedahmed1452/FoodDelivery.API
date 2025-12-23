using FoodDelivery.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Core.Repositories
{
    public interface IGenericRepository<T> where T:BaseEntity
    {
        //five signatures
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetAsync(int id);

    }
}
