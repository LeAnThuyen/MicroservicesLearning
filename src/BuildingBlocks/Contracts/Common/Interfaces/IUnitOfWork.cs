using Microsoft.EntityFrameworkCore;

namespace Contracts.Common.Interfaces
{
    public interface IUnitOfWork<Tcontext> : IDisposable where Tcontext : DbContext
    {
        Task<int> CommitAsync();
    }
}