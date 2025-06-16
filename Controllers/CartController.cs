using LibManage.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibManage.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            var cart = _cartService.GetCartItems();
            return View("Index", cart);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            try
            {
                await _cartService.AddToCartAsync(id);
                TempData["Success"] = "Book added to cart!";
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("ItemAdded", new { id });
        }


        public IActionResult RemoveFromCart(int id)
        {
            _cartService.RemoveFromCart(id);
            return RedirectToAction("Index");
        }

        public IActionResult ItemAdded(int id)
        {
            var cart = _cartService.GetCartItems();
            var item = cart.FirstOrDefault(x => x.BookId == id);
            if (item == null)
            {
                return RedirectToAction("Index");
            }

            return View("ItemAdded", item);
        }

        [HttpPost]
        public async Task<IActionResult> IncreaseQuantity(int id)
        {
            try
            {
                await _cartService.IncreaseQuantityAsync(id);
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("ItemAdded", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> DecreaseQuantity(int id)
        {
            await _cartService.DecreaseQuantityAsync(id);
            return RedirectToAction("ItemAdded", new { id });
        }


    }
}