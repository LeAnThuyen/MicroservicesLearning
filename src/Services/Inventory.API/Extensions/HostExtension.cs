using Inventory.Product.API.Persistence;
using MongoDB.Driver;

namespace Inventory.Product.API.Extensions;

public static class HostExtension
{
    public static IHost MigrateDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var settings = services.GetService<DatabaseSettings>();
        if (settings is null || String.IsNullOrEmpty(settings.ConnectionString))
        {
            throw new ArgumentException("DatabaseSetting is not configured !");
        }

        var mongoClient = services.GetRequiredService<IMongoClient>();
        new InventoryDbSeed().SeedDataAsync(mongoClient, settings).Wait();
        return host;
    }
}