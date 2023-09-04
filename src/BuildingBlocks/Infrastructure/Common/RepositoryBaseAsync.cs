using Contracts.Common.Interfaces;
using Contracts.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Common
{
    public class RepositoryBaseAsync<T, K, TContext> : RepositoryQueryBase<T, K, TContext>, IRepositoryBaseAsync<T, K, TContext> where T : EntityBase<K> where TContext : DbContext
    {

        private readonly TContext _dbContext;
        private readonly IUnitOfWork<TContext> _unitOfWork;

        public RepositoryBaseAsync(TContext dbContext, IUnitOfWork<TContext> unitOfWork) : base(dbContext)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }


        public async Task<IDbContextTransaction> BeginTransactionAsync()
       => await _dbContext.Database.BeginTransactionAsync();

        public async Task<K> CreateAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity.Id;
        }

        public async Task<IList<K>> CreateListAsync(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            return entities.Select(x => x.Id).ToList();
        }

        public Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public Task DeleteListAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;
        }

        public async Task EndTransactionAsync()
        {
            await SaveChangesAsync();
            await _dbContext.Database.CommitTransactionAsync();
        }



        public async Task RollBackTransactionAsync()
       => await _dbContext.Database.RollbackTransactionAsync();

        public Task<int> SaveChangesAsync()
       => _unitOfWork.CommitAsync();

        public Task UpdateAsync(T entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Unchanged) return Task.CompletedTask;
            T exist = _dbContext.Set<T>().Find(entity.Id);
            _dbContext.Entry(exist).CurrentValues.SetValues(entity);
            return Task.CompletedTask;
        }

        public Task UpdateListAsync(IEnumerable<T> entities)
        => _dbContext.Set<T>().AddRangeAsync(entities);
    }
}

