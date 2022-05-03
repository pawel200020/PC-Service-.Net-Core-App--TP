using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authn.Models
{
    public class Repair
    {
        [Key]
        public int Id {get; set; }
        [Display(Name = "Device Brand")]
        [Required(ErrorMessage = "Please select a brand of a device", AllowEmptyStrings = false)]
        public string Brand { get; set; }
        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }
        [Required(ErrorMessage = "Please select a condition of a device", AllowEmptyStrings = false)]
        public string Condition { get; set; }
        [Required(ErrorMessage = "Please describe a problem with your device", AllowEmptyStrings = false)]
        [StringLength(1000, MinimumLength = 20, ErrorMessage = "This filed must contains at least 20 characters")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Select a delivery method", AllowEmptyStrings = false)]
        [Display(Name = "Delivery Method")]
        public string Delivery { get; set; }
        [Display(Name = "Delivery date")]
        [DataType(DataType.Date)]
        public DateTime Date  { get; set; }
        /// <summary>
        /// not public to form
        /// </summary>
        public string ImageName { get; set; }
        [NotMapped]
        [Display(Name = "Upload image with a problem")]
        public IFormFile ImageFile { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public string Report { get; set; }

    }
}
