﻿namespace LibManage.DTOs.Order
{
    public class CreateOrderDto
    {
        public string UserId { get; set; } = string.Empty;
        public List<OrderItemDto> Items { get; set; } = new();
    }
}

