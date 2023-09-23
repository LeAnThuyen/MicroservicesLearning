using Contracts.Domains.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContext : DbContext
    {



        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {

        }



        public DbSet<Ordering.Domain.Entities.Order> Orders { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var modified = ChangeTracker.Entries().
                Where(c => c.State == EntityState.Modified || c.State == EntityState.Added || c.State == EntityState.Deleted);
            foreach (var item in modified)
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        if (item.Entity is IDateTracking addedEntity)
                        {
                            addedEntity.CreatedDate = DateTime.UtcNow;
                            item.State = EntityState.Added;
                        }
                        break;
                    case EntityState.Modified:
                        Entry(item.Entity).Property("Id").IsModified = false;
                        if (item.Entity is IDateTracking modifiedEntity)
                        {
                            modifiedEntity.LastModifiedDate = DateTime.UtcNow;
                            item.State = EntityState.Modified;
                        }
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
