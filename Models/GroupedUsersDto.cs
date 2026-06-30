namespace ObourLand.Models
{
    public class GroupedUsersDto
    {
        public int GroupId { get; set; }
        public string? GroupName { get; set; }
        public List<UserDto>? Users { get; set; }
    }
}
