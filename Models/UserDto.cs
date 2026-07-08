namespace ObourLand.Models
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? GroupName { get; set; }
        public int? RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? SupervisorName { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
