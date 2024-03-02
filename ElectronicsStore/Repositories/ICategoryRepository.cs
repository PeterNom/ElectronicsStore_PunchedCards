using ElectronicsStore.Models;

namespace ElectronicsStore.Repositories
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable<Category>> GetAllAsync();
    }
}
