using System.ComponentModel.DataAnnotations;

namespace ObourLand.Models
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
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
