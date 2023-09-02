using Microsoft.EntityFrameworkCore;

namespace Customer.API.Persistence
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {

        }



        // An Thuyen Le Fuckin Deptrai
        public DbSet<Entities.Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entities.Customer>().HasIndex(c => c.UserName).IsUnique();
            modelBuilder.Entity<Entities.Customer>().HasIndex(c => c.EmailAddress).IsUnique();
        }
    }
}
