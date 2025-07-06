using Entities;
using System.Collections.Generic;

namespace Services
{
    public interface ICartService
    {
        void AddToCart(ProductsModel product, int quantity);
        void RemoveFromCart(ProductsModel product);
        void ClearCart();
        List<CartItem> GetCartItems();
    }
}
