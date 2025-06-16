using System.Collections.Generic;

namespace LibManage.ViewModels
{
    public class CartViewModel
    {
        public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();
        public decimal TotalAmount => Items.Sum(item => item.TotalPrice);

    }
}

