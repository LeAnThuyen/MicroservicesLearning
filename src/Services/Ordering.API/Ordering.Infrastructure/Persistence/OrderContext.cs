using Contracts.Domains.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Contracts.Common.Events;
using Contracts.Common.Interfaces;
using Infrastructure.Extensions;
using MediatR;
using Serilog;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContext : DbContext
    {
        public DbContextOptions<OrderContext> Options { get; private set; }

        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        private List<BaseEvent> _baseEvents;

        private void SetBaseEventBeforeSaveChange()
        {
            var domainEntities =ChangeTracker.Entries<IEventEntity>()
                .Select(x => x.Entity).Where(x => x.DomainEvents().Any()).ToList();
            _baseEvents= domainEntities.SelectMany(c => c.DomainEvents()).ToList();
            foreach (var x in domainEntities)
            {
                x.ClearDomainEvent();
            }
        }
        public OrderContext(DbContextOptions<OrderContext> options, IMediator mediator, ILogger logger) : base(options)
        {
            Options = options;
            _mediator = mediator;
            _logger = logger;
        }



        public DbSet<Ordering.Domain.Entities.Order> Orders { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {

            SetBaseEventBeforeSaveChange();
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
            var result= base.SaveChangesAsync(cancellationToken);
            _mediator.DispatchDomainEventAsync(_baseEvents, _logger);
            return result;
        }
    }
}
