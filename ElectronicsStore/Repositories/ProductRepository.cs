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

        public async Task<IEnumerable<Product>> GetDealsAsync()
        {
            var dealProducts = await _applicationDbContext.Products
                .Include(c => c.Category)
                .Where(p => p.IsDeal ==  true)
                .ToListAsync();
            
            return dealProducts;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var product = await _applicationDbContext.Products
                .Include (c => c.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            return product;
        }

        public async Task<IEnumerable<Product>> SearchProductAsync(string searchQuery)
        {
            var searchResults = await _applicationDbContext.Products.Where(p => p.Name.Contains(searchQuery)).ToListAsync();

            return searchResults;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int id)
        {
            var productsByCategory = await _applicationDbContext.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Where(p => p.CategoryId == id)
                .ToListAsync();
            return productsByCategory;
        }
    }
}
