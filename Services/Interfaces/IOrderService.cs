using LibManage.Models;

namespace LibManage.Services.Interfaces
{
    public interface IOrderService
    {
        void PlaceOrder(string username, List<CartItem> items);
    }
}
