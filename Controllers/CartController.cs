using Microsoft.AspNetCore.Mvc;
using Library_test.Data;
using Library_test.Models;
using Newtonsoft.Json;


namespace Library_test.Controllers
{
    public class CartController : Controller
    {
        public readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var sessionCart = HttpContext.Session.GetString("Cart");
            var cart = sessionCart == null
                ? new List<CartItem>()
                : JsonConvert.DeserializeObject<List<CartItem>>(sessionCart);

            return View(cart);
        }
        [HttpPost]
        public IActionResult Remove(int bookId)
        {
            var sessionCart = HttpContext.Session.GetString("Cart");
            if (sessionCart == null) return RedirectToAction("Index");

            var cart = JsonConvert.DeserializeObject<List<CartItem>>(sessionCart);
            var item = cart.FirstOrDefault(c => c.BookId == bookId);
            if (item != null) cart.Remove(item);

            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index");
        }
    }
}