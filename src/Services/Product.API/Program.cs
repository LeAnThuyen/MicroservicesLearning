using Common.Logging;
using Product.API.Extentions;
using Product.API.Persistence;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();
var builder = WebApplication.CreateBuilder(args);
try
{

    Log.Information($"Start {builder.Environment.ApplicationName} up");

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
}
catch (Exception ex)
{

    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal)) throw;

    Log.Fatal(ex, $"Unhandled exception: {ex.Message}");
}
finally
{
    Log.Information($"Shutdown {builder.Environment.ApplicationName} complete");
    Log.CloseAndFlush();
}




