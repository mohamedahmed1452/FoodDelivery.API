using FoodDelivery.Core;
using FoodDelivery.Core.Entities;
using FoodDelivery.Core.Repositories;
using FoodDelivery.Repository;
using FoodDelivery.Repository.Data;
using System.Collections;

namespace FoodDelivery.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext dbContext;
        private Hashtable Repositories;

        public UnitOfWork(StoreContext dbContext)
        {
            this.dbContext = dbContext;
            Repositories = new Hashtable();
        }

        public IGenericRepository<T> GetRepository<T>() where T : BaseEntity
        {
            var key = typeof(T).Name;
            if (!Repositories.Contains(key))
            {
                var repo = new GenericRepository<T>(dbContext);
                Repositories.Add(key, repo);
            }
            return Repositories[key] as IGenericRepository<T>;
        }



        public Task<int> CompleteAsync()
       => dbContext.SaveChangesAsync();

        public ValueTask DisposeAsync()
       => dbContext.DisposeAsync();
    }
}
