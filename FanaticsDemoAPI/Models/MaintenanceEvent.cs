using System.ComponentModel.DataAnnotations;

namespace FanaticsDemoAPI.Models
{
    public class MaintenanceEvent
    {
        [Key]
        public string EventId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }
        public string Technician { get; set; }
    }
}
