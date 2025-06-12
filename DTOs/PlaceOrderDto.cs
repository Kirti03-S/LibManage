// DTOs/Order/PlaceOrderDto.cs
using LibManage.DTOs.Cart;

namespace LibManage.DTOs.Order
{
    public class PlaceOrderDto
    {
        public List<CartItemDto> CartItems { get; set; }
        public string UserId { get; set; }
    }
}

