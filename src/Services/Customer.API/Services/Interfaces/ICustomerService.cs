namespace Customer.API.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IResult> GetCustomerByUserNameAsync(string userName);
        public Task<IResult> GetCustomersAsync();
    }
}
