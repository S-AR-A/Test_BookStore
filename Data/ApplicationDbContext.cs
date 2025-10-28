
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;
using Library_test.Data;
using Library_test.Models;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace Library_test.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
         DbContextOptions<ApplicationDbContext> options)
         : base(options)
        { }
        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}