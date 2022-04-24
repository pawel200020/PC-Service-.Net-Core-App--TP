using System.ComponentModel.DataAnnotations;

namespace Authn.Models
{
    public class AppUserVM
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please provide username", AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "User Name must be 3 char long.")]
        [Display(Name = "Customized Inherited Name")]
        public string UserName { get; set; }

        public string Provider { get; set; }

        public string NameIdentifier { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "Must be between 8 and 255 characters")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm password is required", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "Must be between 8 and 255 characters")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [EmailAddress]
        [Display(Name = "Email adress")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please provide full name", AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be 3 char long.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please provide full last name", AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Last Name must be 3 char long.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "Not a number")]
        [RegularExpression(@"^\d+$")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(0, 30, ErrorMessage = "Please use values between 0 to 30")]
        [StringLength(9,MinimumLength =9, ErrorMessage ="Phone number must have 9 digits")]
        [Display(Name = "Phone number")]
        public string Mobile { get; set; }

        public string Roles { get; set; }

        public List<string> RoleList
        {
            get
            {
                return Roles.Split(',').ToList();
            }
        }
    }
}
