namespace ObourLand.Models
{
    public class ReportDto
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? X { get; set; }
        public string? Y { get; set; }
        public string? Date { get; set; }
        public string? Time { get; set; }
        public string? Comment { get; set; }
        public string? Image { get; set; }
        public int? SupervisorId { get; set; }
        public string? SupervisorName { get; set; }
        public string? RoleName { get; set; }
    }
}
