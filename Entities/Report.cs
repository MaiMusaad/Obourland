namespace ObourLand.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string X { get; set; }
        public string Y { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Comment { get; set; }
        public string Image { get; set; }

        public User User { get; set; }
    }
}
