using LibManage.Models;
using LibManage.Services.Interfaces;
using System.Text.Json.Serialization;
using System.Text.Json;
using LibManage.Data;

namespace LibManage.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LibraryDbContext _context;
        private const string SessionKey = "Cart";

        public CartService(IHttpContextAccessor httpContextAccessor, LibraryDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        private ISession Session => _httpContextAccessor.HttpContext.Session;

        public void AddToCart(int bookId)
        {
            var cart = GetCartItems();
            var item = cart.FirstOrDefault(i => i.BookId == bookId);

            if (item != null) item.Quantity++;
            else cart.Add(new CartItem { BookId = bookId, Book = _context.Books.Find(bookId), Quantity = 1 });

            SaveCart(cart);
        }

        public void RemoveFromCart(int bookId)
        {
            var cart = GetCartItems();
            var item = cart.FirstOrDefault(i => i.BookId == bookId);
            if (item != null) cart.Remove(item);
            SaveCart(cart);
        }

        public List<CartItem> GetCartItems()
        {
            var cartJson = Session.GetString(SessionKey);
            return string.IsNullOrEmpty(cartJson)
                ? new List<CartItem>()
                : JsonSerializer.Deserialize<List<CartItem>>(cartJson, new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles });
        }

        public void ClearCart() => Session.Remove(SessionKey);

        private void SaveCart(List<CartItem> cart)
        {
            var json = JsonSerializer.Serialize(cart, new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles });
            Session.SetString(SessionKey, json);
        }
    }

}
