using Customer.API.Services.Interfaces;

namespace Customer.API.Controllers
{
    public static class CustomerController
    {
        public static void MapCustomersAPI(this WebApplication app)
        {
            app.Map("/", () => "Welcome to Customer API !");
            app.MapGet("/api/customers", async (ICustomerService customerService) => await customerService.GetCustomersAsync());
            app.MapGet("/api/customers/{username}", async (string username, ICustomerService customerService) => await customerService.GetCustomerByUserNameAsync(username));
            ////app.MapPost("/api/customers", async (Customer.API.Entities.Customer customer, ICustomerRepository customerRepository) =>
            ////{
            ////    customerRepository.CreateAsync(customer);
            ////    customerRepository.SaveChangesAsync();
            ////});
            ////app.MapPut("/api/customers/{id}", () => "Welcome to Customer API !");
            ////app.MapDelete("/api/customers/{id}", async (int id, ICustomerRepository customerRepository) =>
            ////{
            ////    var customer = await customerRepository.FindByCondition(c => c.Id.Equals(id)).SingleOrDefaultAsync();
            ////    if (customer == null)
            ////    {
            ////        return Results.NotFound();
            ////    }

            ////    await customerRepository.DeleteAsync(customer);
            ////    await customerRepository.SaveChangesAsync();
            ////    return Results.NoContent();
            ////});
        }
    }
}
