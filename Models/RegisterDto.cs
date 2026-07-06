using System.ComponentModel.DataAnnotations;

namespace ObourLand.Models
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Username is required.")]
        [RegularExpression(@"^[a-zA-Z0-9._]{3,20}$", ErrorMessage = "Username must be 3 to 20 characters long and can only contain letters, numbers, dots (.), and underscores (_).")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters.")]
        [RegularExpression(@"^\S{6,20}$", ErrorMessage = "Password cannot contain spaces.")]
        public string Password { get; set; } = null!;

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public int RoleId { get; set; }
        public int? GroupId { get; set; }
    }
}
