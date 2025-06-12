using LibManage.ViewModels;

namespace LibManage.Services.Interfaces
{
    public interface ICartService
    {
        List<CartItemViewModel> GetCartItems();
        Task AddToCartAsync(int bookId);

        void RemoveFromCart(int bookId);
        void ClearCart();
    }
}
