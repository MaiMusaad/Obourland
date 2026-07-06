using System.ComponentModel.DataAnnotations;

namespace ObourLand.Models
{
    public class ChangePasswordDTO
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters.")]
        [RegularExpression(@"^\S{6,20}$", ErrorMessage = "Password cannot contain spaces.")]
        public string Password { get; set; } = null!;
    }
}
