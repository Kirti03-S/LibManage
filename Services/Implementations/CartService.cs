using LibManage.Models;
using LibManage.Services.Interfaces;
using LibManage.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace LibManage.Services
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBookService _bookService;
        private const string CartSessionKey = "CartItems";

        public CartService(IHttpContextAccessor httpContextAccessor, IBookService bookService)
        {
            _httpContextAccessor = httpContextAccessor;
            _bookService = bookService;
        }

        public List<CartItemViewModel> GetCartItems()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cartJson = session.GetString(CartSessionKey);
            return string.IsNullOrEmpty(cartJson)
                ? new List<CartItemViewModel>()
                : JsonSerializer.Deserialize<List<CartItemViewModel>>(cartJson)!;
        }

        public async Task AddToCartAsync(int bookId)
        {
            var book = await _bookService.GetBookByIdAsync(bookId);
            if (book == null) return;

            var cart = GetCartItems();
            var existingItem = cart.FirstOrDefault(x => x.BookId == bookId);
            if (existingItem != null)
                existingItem.Stock++;
            else
                cart.Add(new CartItemViewModel
                {
                    BookId = book.Id,
                    Title = book.Title,
                    Price = book.Price,
                    CoverImageUrl = book.CoverImageUrl,
                    Stock = 1
                });

            SaveCart(cart);
        }

        public void RemoveFromCart(int bookId)
        {
            var cart = GetCartItems();
            var item = cart.FirstOrDefault(x => x.BookId == bookId);
            if (item != null)
                cart.Remove(item);
            SaveCart(cart);
        }

        public void ClearCart()
        {
            _httpContextAccessor.HttpContext.Session.Remove(CartSessionKey);
        }

        private void SaveCart(List<CartItemViewModel> cart)
        {
            var cartJson = JsonSerializer.Serialize(cart);
            _httpContextAccessor.HttpContext.Session.SetString(CartSessionKey, cartJson);
        }
    }
}