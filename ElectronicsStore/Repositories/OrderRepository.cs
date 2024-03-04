using ElectronicsStore.Data;
using ElectronicsStore.Models;
using Microsoft.CodeAnalysis;

namespace ElectronicsStore.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IShoppingCart _shoppingCart;

        public OrderRepository(ApplicationDbContext applicationDbContext, IShoppingCart shoppingCart)
        {
            _applicationDbContext = applicationDbContext;
            _shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            List<ShoppingCartItem>? shoppingCartItems = _shoppingCart.ShoppingCartItems;
            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();

            order.OrderDetails = new List<OrderDetail>();

            foreach (var item in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = item.Amount,
                    ProductId = item.Product.ProductId,
                    Price = item.Product.Price
                };
                order.OrderDetails.Add(orderDetail);
            }
            _applicationDbContext.Orders.Add(order);
            _applicationDbContext.SaveChanges();
        }
    }
}
