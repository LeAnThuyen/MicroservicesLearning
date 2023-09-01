using Contracts.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common
{
    public class UnitOfWork<Tcontext> : IUnitOfWork<Tcontext> where Tcontext : DbContext
    {
        private readonly Tcontext _context;

        public UnitOfWork(Tcontext context)
        {
            _context = context;
        }

        public async Task<int> CommitAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();


    }
}
