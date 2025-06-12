using System.Security.Claims;
using System.Text.Json;
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

        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            var cartJson = HttpContext.Session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(cartJson))
            {
                TempData["Error"] = "Your cart is empty.";
                return RedirectToAction("Index", "Cart");
            }

            var cartItems = JsonSerializer.Deserialize<List<CartItemViewModel>>(cartJson);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _orderService.PlaceOrderAsync(userId, cartItems);

            HttpContext.Session.Remove(CartSessionKey);
            TempData["Success"] = "Order placed successfully!";
            return RedirectToAction("MyOrders");
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

