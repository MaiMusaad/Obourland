namespace ObourLand.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? RoleId { get; set; }
        public int? GroupId { get; set; }
        public int? SupervisorId { get; set; }
        public bool? IsActive { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        public Role Role { get; set; }
        public Group Group { get; set; }
        public User Supervisor { get; set; }

    }
}
