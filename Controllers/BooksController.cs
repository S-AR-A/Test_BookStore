using Library_test.Data;
using Library_test.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Library_test.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<IActionResult> Index(string[] selectedGenres)
        {
            var genres = await _context.Books
        .Select(b => b.Genre)
        .Distinct()
        .ToListAsync();

            var booksQuery = _context.Books.AsQueryable();

            if (selectedGenres != null && selectedGenres.Length > 0)
            {
                booksQuery = booksQuery.Where(b => selectedGenres.Contains(b.Genre));
            }

            var viewModel = new BookListViewModel
            {
                Books = await booksQuery.ToListAsync(),
                Genres = genres,
                SelectedGenres = selectedGenres
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        [HttpPost]
    public IActionResult AddToCart(int bookId)
    {
        var book = _context.Books.Find(bookId);
        if (book == null) return NotFound();

        // Get current cart from session
        var sessionCart = HttpContext.Session.GetString("Cart");
        List<CartItem> cart = sessionCart == null 
            ? new List<CartItem>() 
            : JsonConvert.DeserializeObject<List<CartItem>>(sessionCart);

        // Check if book already in cart
        var item = cart.FirstOrDefault(c => c.BookId == bookId);
        if (item != null)
        {
            item.Quantity++;
        }
        else
        {
            cart.Add(new CartItem
            {
                BookId = book.Id,
                Title = book.Title,
                Price = book.Price,
                Quantity = 1
            });
        }

        // Save back to session
        HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));

        return RedirectToAction(nameof(Index));
    }
    }
}