using System.ComponentModel.DataAnnotations;

namespace ObourLand.Models
{
    public class ChangePasswordDTO
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        [MinLength(6, ErrorMessage = "The password must be at least 6 characters long.")] 
        public string Password { get; set; } = null!;
    }
}
