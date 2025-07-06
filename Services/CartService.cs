using Entities;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class CartService : ICartService
    {
        private readonly List<CartItem> _cartItems = new();

        public void AddToCart(ProductsModel product, int quantity)
        {
            var existingItem = _cartItems.FirstOrDefault(ci => ci.Product.Id == product.Id);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                _cartItems.Add(new CartItem
                {
                    Product = product,
                    Quantity = quantity
                });
            }
        }

        public void RemoveFromCart(ProductsModel product)
        {
            var item = _cartItems.FirstOrDefault(ci => ci.Product.Id == product.Id);
            if (item != null)
            {
                _cartItems.Remove(item);
            }
        }

        public void ClearCart()
        {
            _cartItems.Clear();
        }

        public List<CartItem> GetCartItems()
        {
            return _cartItems;
        }
    }
}
