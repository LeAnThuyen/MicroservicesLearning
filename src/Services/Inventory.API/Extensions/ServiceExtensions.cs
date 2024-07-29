using System.Xml.Schema;
using Infrastructure.Extensions;
using MongoDB.Driver;

namespace Inventory.Product.API.Extensions;

public static class ServiceExtensions
{
  
  internal static IServiceCollection AddConfigurationServiceSettings(this IServiceCollection services,
    IConfiguration configuration)
  {
  
            
    var cacheSettings = configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();
    services.AddSingleton(cacheSettings);
    return services;
  }

  private static string getMongoConnectionString(this IServiceCollection services)
  {
    var settings = services.GetOptions<DatabaseSettings>(nameof(DatabaseSettings));
    if (settings is null || String.IsNullOrEmpty(settings.ConnectionString))
    {
      throw new ArgumentException("DatabaseSetting is not configured !");
    }

    var databaseName = settings.DatabaseName;
    var mongoDbConnectionString = settings.ConnectionString +"/" +databaseName +"?authSource=admin";
    return mongoDbConnectionString;

  }
  public static void ConfigureMongoDbClient(this IServiceCollection services)
  {
    services.AddSingleton<IMongoClient>(
      new MongoClient(getMongoConnectionString(services))).AddScoped(x=>x.GetService<IMongoClient>()?.StartSession());
  }
  public static void AddInfrastructureServices(this IServiceCollection services)
  {
    services.AddAutoMapper(cfg=>cfg.AddProfile(new MappingProfile()));
  }
}