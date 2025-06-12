using LibManage.Data;
using LibManage.Models;
using LibManage.Services.Interfaces;
using LibManage.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LibManage.Services
{
    public class OrderService : IOrderService
    {
        private readonly LibraryDbContext _context;

        public OrderService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task PlaceOrderAsync(string userId, List<CartItemViewModel> cartItems)
        {
            if (cartItems == null || !cartItems.Any())
                throw new Exception("Cart is empty.");

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                Items = new List<OrderItem>(),
                TotalAmount = 0
            };

            foreach (var item in cartItems)
            {
                var book = await _context.Books.FindAsync(item.BookId);
                if (book == null || book.Stock < item.Stock)
                    throw new Exception($"Book {item.Title} is not available in required quantity.");

                book.Stock -= item.Stock;

                order.Items.Add(new OrderItem
                {
                    BookId = item.BookId,
                    BookTitle = book.Title, // ✅ set BookTitle manually here
                    Stock = item.Stock,
                    Price = item.Price
                });

                order.TotalAmount += item.Price * item.Stock;
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderViewModel>> GetOrdersByUserAsync(string userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.Items) // ✅ valid
                .Select(o => new OrderViewModel
                {
                    OrderId = o.Id,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Items = o.Items.Select(i => new OrderItemViewModel
                    {
                        BookTitle = i.BookTitle, // ✅ use stored title
                        Stock = i.Stock,
                        Price = i.Price
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<List<OrderViewModel>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.Items) // ✅ valid
                .Select(o => new OrderViewModel
                {
                    OrderId = o.Id,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    UserId = o.UserId,
                    Items = o.Items.Select(i => new OrderItemViewModel
                    {
                        BookTitle = i.BookTitle,
                        Stock = i.Stock,
                        Price = i.Price
                    }).ToList()
                })
                .ToListAsync();
        }
    }
}
