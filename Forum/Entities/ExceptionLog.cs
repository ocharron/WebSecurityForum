namespace Forum.Entities
{
    public class ExceptionLog
    {
        public int Id { get; set; }
        public string? RequestPath { get; set; }
        public string? Source { get; set; }
        public string? StackTrace { get; set; }
        public string? Message { get; set; }
        public string? InnerStackTrace { get; set; }
        public string? InnerMessage { get; set; }
        public string? UserId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
