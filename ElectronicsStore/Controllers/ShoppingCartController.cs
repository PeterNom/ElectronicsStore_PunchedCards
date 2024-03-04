using ElectronicsStore.Models.ViewModels;
using ElectronicsStore.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicsStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IShoppingCart _shoppingCart;

        public ShoppingCartController(IProductRepository productRepository, IShoppingCart shoppingCart)
        {
            _productRepository = productRepository;
            _shoppingCart = shoppingCart;
        }

        public async Task<ViewResult> Index()
        {
            var items = await _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel(_shoppingCart, _shoppingCart.GetShoppingCartTotal());

            return View(shoppingCartViewModel);
        }

        public async Task<RedirectToActionResult> AddToShoppingCart(int productId)
        {
            var prod = await _productRepository.GetProductByIdAsync(productId);

            if (prod != null) 
            {
                _shoppingCart.AddToCart(prod);
            }
            return RedirectToAction("Index");
        }

        public async Task<RedirectToActionResult> RemoveFromShoppingCart(int productId)
        {
            var prod = await _productRepository.GetProductByIdAsync(productId);

            if(prod != null)
            {
                await _shoppingCart.RemoveFromCartAsync(prod);
            }

            return RedirectToAction("Index");
        }
    }
}
