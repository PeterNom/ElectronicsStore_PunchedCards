using ElectronicsStore.Models;

namespace ElectronicsStore.Repositories
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}
