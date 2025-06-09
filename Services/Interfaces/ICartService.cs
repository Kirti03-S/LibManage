using LibManage.Models;

namespace LibManage.Services.Interfaces
{
    public interface ICartService
    {
        void AddToCart(int bookId);
        void RemoveFromCart(int bookId);
        List<CartItem> GetCartItems();
        void ClearCart();
    }

}
