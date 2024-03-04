using ElectronicsStore.Models;
using System.IO.Pipelines;

namespace ElectronicsStore.Repositories
{
    public interface IShoppingCart
    {
        void AddToCart(Product product);
        Task<int> RemoveFromCart(Product product);
        List<ShoppingCartItem> GetShoppingCartItems();
        void ClearCart();
        decimal GetShoppingCartTotal();
        Task<List<ShoppingCartItem>> ShoppingCartItems { get; set; }
    }
}
