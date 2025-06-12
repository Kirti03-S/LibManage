// DTOs/Cart/CartItemDto.cs
namespace LibManage.DTOs.Cart
{
    public class CartItemDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}

