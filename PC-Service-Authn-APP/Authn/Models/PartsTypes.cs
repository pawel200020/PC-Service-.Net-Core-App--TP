using System.ComponentModel.DataAnnotations;

namespace Authn.Models
{
    public class PartsTypes
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
