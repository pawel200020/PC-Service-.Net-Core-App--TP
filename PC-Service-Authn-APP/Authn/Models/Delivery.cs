using System.ComponentModel.DataAnnotations;

namespace Authn.Models
{
    public class Delivery
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}
