using Microsoft.EntityFrameworkCore;
using Product.API.Entities;
namespace Product.API.Persistence
{
    public class ProductContextSeed
    {
        public static async Task SeedProductAsync(ProductContext productContext, Serilog.ILogger logger)
        {
            if (!await productContext.Products.AnyAsync())
            {
                await productContext.AddRangeAsync(getCatalogProducts());
                await productContext.SaveChangesAsync();

                logger.Information($"Seeded data for product DB associated with context {nameof(productContext)}", nameof(productContext));
            }
        }

        private static IEnumerable<CatalogProduct> getCatalogProducts()
        {
            return new List<CatalogProduct>
            {
                new()
                {
                    No="Dior Sauvage Elexir",
                    Name="Dior Sauvage Elexir",
                    Summary="For Men",
                    Description="Dior Sauvage Elexir Not For Sell =))",
                    Price=(decimal)121212.6712,


                }
            };

        }
    }

}
