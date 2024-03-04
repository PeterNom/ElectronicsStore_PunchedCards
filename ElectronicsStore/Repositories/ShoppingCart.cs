using ElectronicsStore.Data;
using ElectronicsStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsStore.Repositories
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public string? ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;
        private ShoppingCart(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

            ApplicationDbContext context = services.GetService<ApplicationDbContext>() 
                ?? throw new Exception("Error initializing");

            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

            session?.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Product product)
        {
            var cartItem = _applicationDbContext.ShoppingCartItems
                .FirstOrDefault(sh=> sh.Product.ProductId == product.ProductId && ShoppingCartId== sh.ShoppingCartId);

            if(cartItem == null)
            {
                cartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Amount = 1
                };
                _applicationDbContext.ShoppingCartItems.Add(cartItem);
            }
            else
            {
                cartItem.Amount++;
            }
            _applicationDbContext.SaveChanges();
        }

        public async Task<int> RemoveFromCartAsync(Product product)
        {
            var cartItem = _applicationDbContext.ShoppingCartItems
                .SingleOrDefault(sh => sh.ShoppingCartId == ShoppingCartId && sh.Product.ProductId == product.ProductId);

            var localAmount = 0;

            if (cartItem != null)
            {
                if (cartItem.Amount > 1)
                {
                    cartItem.Amount--;
                    localAmount = cartItem.Amount;
                }
                else
                {
                    _applicationDbContext.ShoppingCartItems.Remove(cartItem);
                }
            }
            await _applicationDbContext.SaveChangesAsync();

            return localAmount;
        }

        public async Task<List<ShoppingCartItem>> GetShoppingCartItems()
        {
            return await _applicationDbContext.ShoppingCartItems
                .Where(c => c.ShoppingCartId == ShoppingCartId)
                .Include(p => p.Product)
                .ToListAsync();
        }

        public void ClearCart()
        {
            var cartItems = _applicationDbContext.ShoppingCartItems
                .Where(sh => sh.ShoppingCartId == ShoppingCartId);
            
            _applicationDbContext.ShoppingCartItems.RemoveRange(cartItems);

            _applicationDbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _applicationDbContext.ShoppingCartItems
                .Where(sh => sh.ShoppingCartId == ShoppingCartId)
                .Select(sh => sh.Amount * sh.Product.Price)
                .Sum();

            return total;
        }

        
    }
}
