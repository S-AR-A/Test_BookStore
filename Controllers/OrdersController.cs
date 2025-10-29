using Library_test.Data;
using Library_test.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Library_test.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;

        }

        public IActionResult Index()
        {
            return View(_context.Orders.ToList());
        }
    }
}