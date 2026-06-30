namespace ObourLand.Models
{
    public class CreateReportDto
    {
        public int UserId { get; set; }
        public string X { get; set; }
        public string Y { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Comment { get; set; }
        public ICollection<IFormFile> Images { get; set; }
    }
}
