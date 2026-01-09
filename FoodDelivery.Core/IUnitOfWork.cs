using FoodDelivery.Core.Entities;
using FoodDelivery.Core.Repositories;

namespace FoodDelivery.Core
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task<int> CompleteAsync();
        IGenericRepository<T> GetRepository<T>() where T : BaseEntity;

    }
}
