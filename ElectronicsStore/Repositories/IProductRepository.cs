using ElectronicsStore.Models;

namespace ElectronicsStore.Repositories
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetDealsAsync();
        public Task<Product?> GetProductByIdAsync(int id);
        public Task<IEnumerable<Product>> SearchProductAsync(string searchQuery);
        public Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
    }
}
