using Common.Logging;
using Contracts.Common.Interfaces;
using Customer.API.Controllers;
using Customer.API.Persistence;
using Customer.API.Repositories;
using Customer.API.Repositories.Interfaces;
using Customer.API.Services;
using Customer.API.Services.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
try
{

    Log.Information($"Start {builder.Environment.ApplicationName} Minimal up");
    // DI Serilog
    builder.Host.UseSerilog(Serilogger.Configure);


    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddScoped<ICustomerRepository, CustomerRepository>()
                .AddScoped(typeof(IRepositoryQueryBaseAsync<,,>), typeof(RepositoryQueryBase<,,>))
                .AddScoped<ICustomerService, CustomerService>();

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
    builder.Services.AddDbContext<CustomerContext>(options => options.UseNpgsql(connectionString));
    var app = builder.Build();
    app.MapCustomersAPI();
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json",
             $"{builder.Environment.ApplicationName} Minimal v1"));
        });
    }

    //app.UseHttpsRedirection(); Production Only

    app.UseAuthorization();
    app.SeedCustomerData();
    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{

    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal)) throw;

    Log.Fatal(ex, $"Unhandled exception: {ex.Message}");
}
finally
{
    Log.Information($"Shutdown {builder.Environment.ApplicationName} Minimal complete");
    Log.CloseAndFlush();
}


