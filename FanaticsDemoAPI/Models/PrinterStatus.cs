using System.ComponentModel.DataAnnotations;

namespace FanaticsDemoAPI.Models
{
    public class PrinterStatus
    {
        [Key]
        public string StatusId { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
