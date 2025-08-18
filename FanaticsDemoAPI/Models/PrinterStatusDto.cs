namespace FanaticsDemoAPI.Models
{
    public class PrinterStatusDto
    {
        public string PrinterId { get; set; }
        public string MachineName { get; set; }
        public string Status { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
