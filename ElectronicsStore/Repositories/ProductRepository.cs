using ElectronicsStore.Data;
using ElectronicsStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ElectronicsStore.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProductRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Product>> GetDeals()
        {
            var dealProducts = await _applicationDbContext.Products
                .Include(c => c.Category)
                .Where(p => p.IsDeal ==  true)
                .ToListAsync();
            
            return dealProducts;
        }

        public async Task<Product?> GetProductById(int id)
        {
            var product = await _applicationDbContext.Products
                .FirstOrDefaultAsync(p => p.ProductId == id);

            return product;
        }

        public async Task<IEnumerable<Product>> SearchProduct(string searchQuery)
        {
            var searchResults = await _applicationDbContext.Products.Where(p => p.Name.Contains(searchQuery)).ToListAsync();

            return searchResults;
        }
    }
}
