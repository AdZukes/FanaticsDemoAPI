using System.ComponentModel.DataAnnotations;

namespace FanaticsDemoAPI.Models
{
    public class PrinterError
    {
        [Key]
        public string ErrorCodeId { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
