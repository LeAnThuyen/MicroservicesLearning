using Common.Logging;
using Product.API.Extentions;
using Product.API.Persistence;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();
Log.Information("Starting Product Product API By Tana Command Pro !");

var builder = WebApplication.CreateBuilder(args);

// DI Serilog
builder.Host.UseSerilog(Serilogger.Configure);
//Service Extentions
builder.Host.AddAppConfigurations();
//Service Extentions
builder.Services.AddInfrastructure(builder.Configuration);
var app = builder.Build();


//Service Extentions
app.UseInfrastucture();
app.MirgrateDatabase<ProductContext>((context, _) =>
{
    ProductContextSeed.SeedProductAsync(context, Log.Logger).Wait();
}).Run();
app.Run();



