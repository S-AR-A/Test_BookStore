using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Library_test.Models;

namespace Library_test.Models
{
public class OrderItem
{
    public int Id { get; set; }

    [Required]
    public int OrderId { get; set; }
    public Order Order { get; set; }

    [Required]
    public int BookId { get; set; }
    public Book Book { get; set; }

    [Range(1, 100)]
    public int Quantity { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal UnitPrice { get; set; }

    [NotMapped]
    public decimal LinePrice => Quantity * UnitPrice;

}
}