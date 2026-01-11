using FoodDelivery.Core.Entities;
using FoodDelivery.Core.Repositories;
using FoodDelivery.Core.specifications;
using FoodDelivery.Infrastructure;
using FoodDelivery.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            //if (typeof(T) == typeof(Product))
            //    return (IReadOnlyList<T>) await _dbContext.Products.Include(p => p.Category).Include(p => p.Brand).ToListAsync() ;
            return await _dbContext.Set<T>().ToListAsync();
        }
        public async Task<T?> GetAsync(int id)
        {
            //if (typeof(T) == typeof(Product))
            //    return await _dbContext.Products.Include(p => p.Category).Include(p => p.Brand).FirstOrDefaultAsync(p => p.Id == id) as T;
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
        {
            return await SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec).AsNoTracking().ToListAsync();
        }
        public async Task<T?> GetWithSpecAsync(ISpecifications<T> spec)
        {
            return await SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec).AsNoTracking().FirstOrDefaultAsync();
        }

        public Task<int> CountAsync(ISpecifications<T> spec)
        {
            return SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec).CountAsync();
        }

        public async Task AddAsync(T item)
        => await _dbContext.Set<T>().AddAsync(item);




        public void Update(T item)
         => _dbContext.Set<T>().Update(item);

        public void Delete(T item)
       => _dbContext.Set<T>().Remove(item);
    }
}
