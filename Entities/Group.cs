namespace ObourLand.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public DateTime? CreatedOn { get; set; }


        public ICollection<User> Users { get; set; }
    }
}
