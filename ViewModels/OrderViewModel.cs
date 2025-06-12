namespace LibManage.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string? UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemViewModel> Items { get; set; }
    }
}
    