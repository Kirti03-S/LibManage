namespace LibManage.DTOs.Order
{

    public class OrderItemDto
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}

