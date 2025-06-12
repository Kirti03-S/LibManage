using LibManage.ViewModels;

namespace LibManage.Services.Interfaces
{
    public interface IOrderService
    {
        Task PlaceOrderAsync(string userId, List<CartItemViewModel> cartItems);
        Task<List<OrderViewModel>> GetOrdersByUserAsync(string userId);
        Task<List<OrderViewModel>> GetAllOrdersAsync();
    }
}
    
