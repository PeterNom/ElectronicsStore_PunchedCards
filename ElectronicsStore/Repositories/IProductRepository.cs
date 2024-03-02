using ElectronicsStore.Models;

namespace ElectronicsStore.Repositories
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetDeals();
        public Task<Product?> GetProductById(int id);

        public Task<IEnumerable<Product>> SearchProduct(string searchQuery);
    }
}
