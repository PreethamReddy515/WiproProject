using System.ComponentModel.DataAnnotations;

namespace SecureLoginMVC.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(120)]
        public string Name { get; set; } = string.Empty;

        [Required, Range(0.01, 1_000_000)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}
