using System.Security.Claims;
using System.Text.Json;
using LibManage.Services;
using LibManage.Services.Interfaces;
using LibManage.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibManage.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private const string CartSessionKey = "Cart";

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            var cartJson = HttpContext.Session.GetString("CartItems");
            if (string.IsNullOrEmpty(cartJson))
            {
                TempData["Error"] = "Your cart is empty.";
                return RedirectToAction("Index", "Cart");
            }

            var cartItems = JsonSerializer.Deserialize<List<CartItemViewModel>>(cartJson);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                await _orderService.PlaceOrderAsync(userId, cartItems);

                HttpContext.Session.Remove("CartItems"); // ✅ Corrected key to match your code

                // ✅ Prepare confirmation data for the success view
                var orderSummary = cartItems.Select(item => new OrderSuccessViewModel
                {
                    Title = item.Title,
                    CoverImageUrl = item.CoverImageUrl,
                    Price = item.Price,
                    Quantity = item.Stock
                }).ToList();

                return View("OrderSuccess", orderSummary); 
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error: {ex.Message}";
                return RedirectToAction("Index", "Cart");
            }
        }



        public async Task<IActionResult> MyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderService.GetOrdersByUserAsync(userId);
            return View(orders);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return View(orders);
        }

        public async Task<IActionResult> CheckOut()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderService.GetOrdersByUserAsync(userId);
            return View(orders);
        }

        

    }
}

