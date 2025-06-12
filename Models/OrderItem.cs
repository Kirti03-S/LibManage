using LibManage.Models.LibManage.Models;

namespace LibManage.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public Order Order { get; set; }
    }
}
