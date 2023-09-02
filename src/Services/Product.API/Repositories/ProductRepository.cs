using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Product.API.Entities;
using Product.API.Persistence;
using Product.API.Repositories.Interfaces;

namespace Product.API.Repositories
{
    public class ProductRepository : RepositoryBaseAsync<CatalogProduct, long, ProductContext>, IProductRepository
    {
        public ProductRepository(ProductContext dbContext, IUnitOfWork<ProductContext> unitOfWork) : base(dbContext, unitOfWork)
        {

        }

        public Task CreateProductAsync(CatalogProduct product) => CreateAsync(product);

        public async Task DeleteProductAsync(long id)
        {
            var product = await GetProductAsync(id);
            if (product != null) await DeleteAsync(product);
        }

        public async Task<CatalogProduct> GetProductAsync(long id) => await GetByIdAsync(id);

        public async Task<CatalogProduct> GetProductByNoAsync(string noProduct) => await FindByCondition(c => c.No.Equals(noProduct)).SingleOrDefaultAsync();



        public async Task<IEnumerable<CatalogProduct>> GetProductsAsync() => await FindAll().ToListAsync();


        public Task UpdateProductAsync(CatalogProduct product) => UpdateAsync(product);

    }
}
