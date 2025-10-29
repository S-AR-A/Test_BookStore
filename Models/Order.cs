using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_test.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Customer Email")]
        public string CustomerEmail { get; set; }

        [Required]
        [StringLength(400)]
        public string Address { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public List<OrderItem> OrderItems { get; set; } = new();

        [Display(Name = "Total Price")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; }
    }
}