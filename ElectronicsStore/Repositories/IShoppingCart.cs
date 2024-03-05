using ElectronicsStore.Models;
using System.IO.Pipelines;

namespace ElectronicsStore.Repositories
{
    public interface IShoppingCart
    {
        void AddToCart(Product product);
        Task<int> RemoveFromCartAsync(Product product);
        Task<List<ShoppingCartItem>> GetShoppingCartItemsAsync();
        List<ShoppingCartItem> GetShoppingCartItems();
        void ClearCart();
        decimal GetShoppingCartTotal();
        List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
