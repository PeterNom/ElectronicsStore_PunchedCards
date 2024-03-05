using ElectronicsStore.Models.ViewModels;
using ElectronicsStore.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicsStore.Components
{
    public class ShoppingCartSummary: ViewComponent
    {
        private readonly IShoppingCart _shoppingCart;

        public ShoppingCartSummary(IShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public async Task<IViewComponentResult> Invoke()
        {
            var items = await _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel(_shoppingCart, _shoppingCart.GetShoppingCartTotal());

            return View(shoppingCartViewModel);
        }
    }
}
