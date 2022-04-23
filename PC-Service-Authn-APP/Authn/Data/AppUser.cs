using System.ComponentModel.DataAnnotations;

namespace Authn.Data
{
    public class AppUser
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please provide username", AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "User Name must be 3 char long.")]
        public string UserName { get; set; }
        public string Provider { get; set; }
        public string NameIdentifier { get; set; }
        [Required(ErrorMessage = "Please provide Password", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be 8 char long.")]
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please provide full name", AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be 3 char long.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please provide full last name", AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Last Name must be 3 char long.")]
        public string LastName { get; set; }
        [Phone]
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
