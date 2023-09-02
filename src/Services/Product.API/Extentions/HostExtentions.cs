using Microsoft.EntityFrameworkCore;

namespace Product.API.Extentions
{
    public static class HostExtentions
    {
        public static IHost MirgrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var cofiguration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();
                try
                {
                    logger.LogInformation("Mirgrating My SQL Database");
                    ExcuteMigration(context);
                    logger.LogInformation("Mirgrated My SQL Database");
                    InvokeSeeder(seeder, context, services);
                }
                catch (Exception ex)
                {

                    logger.LogError(ex, "An error occurred while migrating the my sql database");
                }
            }
            return host;
        }

        private static void ExcuteMigration<TContext>(TContext context) where TContext : DbContext
        {
            context.Database.Migrate();
        }

        private static void InvokeSeeder<Tcontext>(Action<Tcontext, IServiceProvider> seeder, Tcontext context, IServiceProvider serviceProvider) where Tcontext : DbContext
        {
            seeder(context, serviceProvider);
        }

    }
}
