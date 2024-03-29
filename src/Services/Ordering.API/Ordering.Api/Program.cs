using Common.Logging;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Persistence;
using Ordrering.API.Extentions;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

Log.Information($"Start {builder.Environment.ApplicationName} up");
// Add Middleware Appsettings 
builder.Host.AddAppConfigurations();
// Add Middleware SMTP Email 
builder.Services.AddConfigurationServiceSettings(builder.Configuration);
// Add Middleware Service 
builder.Services.AddInfrastructureService(builder.Configuration);

builder.Services.AddApplicationServices();
builder.Services.AddApplicationServices();
// DI Serilog
builder.Host.UseSerilog(Serilogger.Configure);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var orderContextSeed = scope.ServiceProvider.GetRequiredService<OrderContextSeed>();
    await orderContextSeed.InitialiseAsync();
    await orderContextSeed.SeedAsync();
}

app.UseRouting();
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



