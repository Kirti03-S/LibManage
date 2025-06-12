using LibManage.Models;

namespace LibManage.Services.Interfaces
{
    public interface ICartService
    {
        List<CartItem> GetCartItems();
        void AddToCart(CartItem item);
        void RemoveFromCart(int bookId);
        void ClearCart();
    }
}

