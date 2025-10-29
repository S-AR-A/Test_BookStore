using Library_test.Data;
using Library_test.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_test.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;

        }

        public IActionResult Index()
        {
            return View(_context.Orders.ToList());
        }
    }
}