using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;
using Serilog;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        private readonly ILogger _logger;
        private readonly OrderContext _context;

        public OrderContextSeed(ILogger logger, OrderContext context)
        {
            _logger = logger;
            _context = context;
        }


        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An error occurred while intialising the orderdb.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            if (!_context.Orders.Any())
            {
                await _context.Orders.AddRangeAsync(new Order
                {
                    UserName = "Customer1",
                    FirstName = "Tana Fucking Hand Some",
                    LastName = "Le",
                    EmailAddress = "leanthuyen.working@gmail.com",
                    ShippingAddress = "Park Jang Inda Fucking Hood",
                    InvoiceAddress = "VietNam",
                    TotalPrice = 120,
                },
                new Order
                {
                    UserName = "Customer2",
                    FirstName = "An Thuyen",
                    LastName = "Le",
                    EmailAddress = "leanthuyen.working@gmail.com",
                    ShippingAddress = "Park Jang Inda Fucking Hood",
                    InvoiceAddress = "VietNam",
                    TotalPrice = 11231,
                },
                new Order
                {
                    UserName = "Customer3",
                    FirstName = "Nhang An",
                    LastName = "Le",
                    EmailAddress = "leanthuyen.working@gmail.com",
                    ShippingAddress = "Park Jang Inda Fucking Hood",
                    InvoiceAddress = "VietNam",
                    TotalPrice = 9999,
                }
                );
            }
        }
    }
}
