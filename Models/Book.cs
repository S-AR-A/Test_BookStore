using System.ComponentModel.DataAnnotations;

namespace Library_test.Models
{

     public  class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(70)]
        public string Auther { get; set; }

        [DataType(DataType.Date)]
        public DateTime Publish_Date { get; set; }

        [Range(0, 999.99)]
        public decimal Price { get; set; }
        public string Genre { get; set; }
    }
}