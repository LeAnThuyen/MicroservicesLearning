using Contracts.Common.Interfaces;
using Product.API.Entities;
using Product.API.Persistence;

namespace Product.API.Repositories.Interfaces
{
    public interface IProductRepository : IRepositoryBaseAsync<CatalogProduct, long, ProductContext>
    {
        public Task<IEnumerable<CatalogProduct>> GetProductsAsync();
        public Task<CatalogProduct> GetProductAsync(long id);
        public Task<CatalogProduct> GetProductByNoAsync(string noProduct);
        public Task CreateProductAsync(CatalogProduct product);
        public Task UpdateProductAsync(CatalogProduct product);
        public Task DeleteProductAsync(long id);

    }
}
