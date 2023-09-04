using Contracts.Common.Interfaces;
using Contracts.Domains;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Common
{
    public class RepositoryQueryBase<T, K, TContext> : IRepositoryQueryBaseAsync<T, K, TContext> where T : EntityBase<K> where TContext : DbContext
    {


        private readonly TContext _dbContext;

        public RepositoryQueryBase(TContext dbcontext)
        {
            _dbContext = dbcontext;
        }



        public IQueryable<T> FindAll(bool trackChanges = false) => !trackChanges ? _dbContext.Set<T>().AsNoTracking() : _dbContext.Set<T>();

        public IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
        {
            var items = FindAll(trackChanges);
            items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
            return items;
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
        {
            var items = FindByCondition(expression, trackChanges);
            items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
            return items;
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false)
        => !trackChanges ? _dbContext.Set<T>().Where(expression).AsNoTracking() : _dbContext.Set<T>().Where(expression);

        public async Task<T?> GetByIdAsync(K id)
         => await FindByCondition(c => c.Id.Equals(id)).FirstOrDefaultAsync();


        public async Task<T?> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties)
             => await FindByCondition(c => c.Id.Equals(id), trackChanges: false, includeProperties).FirstOrDefaultAsync();

        public async Task RollBackTransactionAsync()
       => await _dbContext.Database.RollbackTransactionAsync();


    }
}
