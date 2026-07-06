using System.ComponentModel.DataAnnotations;

namespace ObourLand.Models
{
    public class UpdateUserDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be greater than 0.")]
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        public int SupervisorId { get; set; }
    }
}
