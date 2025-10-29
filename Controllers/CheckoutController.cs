using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Library_test.Data;
using Library_test.Models;
using Microsoft.EntityFrameworkCore;

namespace YourAppName.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CheckoutController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var sessionCart = HttpContext.Session.GetString("Cart");
            var cart = sessionCart == null
                ? new List<CartItem>()
                : JsonConvert.DeserializeObject<List<CartItem>>(sessionCart);

            if (!(cart.Count > 0))
                return RedirectToAction("Index", "Cart");

            return View(new Order()); // empty order for form binding
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Order order)
        {
            var sessionCart = HttpContext.Session.GetString("Cart");
            var cart = sessionCart == null
                ? new List<CartItem>()
                : JsonConvert.DeserializeObject<List<CartItem>>(sessionCart);

            if (!cart.Any())
            {
                ModelState.AddModelError("", "Your cart is empty.");
                return View(order);
            }

            if (ModelState.IsValid)
            {
                var total = cart.Sum(c => c.Price * c.Quantity);
                var newOrder = new Order
                {
                    CustomerName = order.CustomerName,
                    CustomerEmail = order.CustomerEmail,
                    Address = order.Address,
                    OrderDate = DateTime.Now,
                    TotalPrice = total,
                    OrderItems = [.. cart.Select(c => new OrderItem
                    {
                        BookId = c.BookId,
                        Quantity = c.Quantity,
                        UnitPrice = c.Price
                    })]
                };

                _context.Orders.Add(newOrder);
                _context.SaveChanges();

                HttpContext.Session.Remove("Cart");

                TempData["SuccessMessage"] = "Order placed successfully!";
                return RedirectToAction("Success", new { orderId = newOrder.Id });
            }

            return View(order);
        }

        public IActionResult Success(int orderId)
        {
            var order = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Book)
                .FirstOrDefault(o => o.Id == orderId);

            if (order == null)
                return NotFound();

            return View(order);
        }
    }
}
