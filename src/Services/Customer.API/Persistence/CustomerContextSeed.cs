using Microsoft.EntityFrameworkCore;

namespace Customer.API.Persistence
{
    public static class CustomerContextSeed
    {
        public static IHost SeedCustomerData(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var customerContext = scope.ServiceProvider.GetRequiredService<CustomerContext>();
            customerContext.Database.MigrateAsync().GetAwaiter().GetResult();
            CreateCustomer(customerContext, "cus1001", "Le",
              "An Thuyen", "leanthuyen@gmail.com")
          .GetAwaiter().GetResult();
            CreateCustomer(customerContext, "cus10012", "Unknow",
                    "Stranger", "stranger@gmail.com")
                .GetAwaiter().GetResult();

            return host;
        }

        public static async Task CreateCustomer(CustomerContext customerContext, string username, string firstname, string lastname, string emailaddress)
        {
            var customer = await customerContext.Customers
             .SingleOrDefaultAsync(x => x.UserName.Equals(username) ||
             x.EmailAddress.Equals(emailaddress));
            if (customer == null)
            {
                var newCustomer = new Entities.Customer
                {
                    UserName = username,
                    FirstName = firstname,
                    LastName = lastname,
                    EmailAddress = emailaddress,
                };
                await customerContext.Customers.AddAsync(newCustomer);
                await customerContext.SaveChangesAsync();
            }
        }
    }


}
