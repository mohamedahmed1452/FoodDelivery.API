using FoodDelivery.Core.Entities;
using FoodDelivery.Core.specifications;

namespace FoodDelivery.Core.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        //five signatures
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetAsync(int id);
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec);
        Task<T?> GetWithSpecAsync(ISpecifications<T> spec);
        Task<int> CountAsync(ISpecifications<T> spec);

        Task AddAsync(T item);
        void Update(T item);
        void Dalata(T item);


    }
}
