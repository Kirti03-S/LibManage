using LibManage.Models;
using LibManage.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace LibManage.Services
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CartSessionKey = "CartItems";

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<CartItem> GetCartItems()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cartJson = session.GetString(CartSessionKey);
            return string.IsNullOrEmpty(cartJson) ? new List<CartItem>() : JsonSerializer.Deserialize<List<CartItem>>(cartJson);
        }

        public void AddToCart(CartItem item)
        {
            var cart = GetCartItems();
            var existingItem = cart.FirstOrDefault(x => x.BookId == item.BookId);
            if (existingItem != null)
            {
                existingItem.Stock += item.Stock;
            }
            else
            {
                cart.Add(item);
            }
            SaveCart(cart);
        }

        public void RemoveFromCart(int bookId)
        {
            var cart = GetCartItems();
            var item = cart.FirstOrDefault(x => x.BookId == bookId);
            if (item != null) cart.Remove(item);
            SaveCart(cart);
        }

        public void ClearCart()
        {
            _httpContextAccessor.HttpContext.Session.Remove(CartSessionKey);
        }

        private void SaveCart(List<CartItem> cart)
        {
            var cartJson = JsonSerializer.Serialize(cart);
            _httpContextAccessor.HttpContext.Session.SetString(CartSessionKey, cartJson);
        }
    }
}

