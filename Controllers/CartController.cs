using LibManage.Models;
using LibManage.Services.Interfaces;
using LibManage.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibManage.Controllers
{
    public class CartController : Controller
    {
        private readonly IBookService _bookService;
        private const string SessionKey = "CartItems";

        public CartController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItemViewModel>>(SessionKey) ?? new List<CartItemViewModel>();
            return View("Index",cart);
        }
        //[HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null) return NotFound();

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItemViewModel>>(SessionKey) ?? new List<CartItemViewModel>();

            var existingItem = cart.FirstOrDefault(c => c.BookId == id);
            if (existingItem != null)
                existingItem.Stock++;
            else
                cart.Add(new CartItemViewModel
                {
                    BookId = book.Id,
                    Title = book.Title,
                    Price = book.Price,
                    Stock = 1
                });

            HttpContext.Session.SetObjectAsJson(SessionKey, cart);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItemViewModel>>(SessionKey) ?? new List<CartItemViewModel>();
            var item = cart.FirstOrDefault(c => c.BookId == id);
            if (item != null)
                cart.Remove(item);

            HttpContext.Session.SetObjectAsJson(SessionKey, cart);
            return RedirectToAction("Index");
        }
    }
}
