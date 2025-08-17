using System.ComponentModel.DataAnnotations;

namespace FanaticsDemoAPI.Models
{
    public class OffsetPrinter
    {
        [Key]
        public string PrinterId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Model { get; set; }
        public string Location { get; set; }
        public string JobId { get; set; }
        public string JobName { get; set; }
        public DateTime JobStartTime { get; set; }
        public DateTime? JobEndTime { get; set; }
        public int TotalPagesPrinted { get; set; }
        public string PaperType { get; set; }
        public string InkType { get; set; }
        public double InkUsageLiters { get; set; }
        public double PaperWasteKg { get; set; }
        public TimeSpan Downtime { get; set; }
        public double EnergyConsumptionKWh { get; set; }
        public List<MaintenanceEvent> MaintenanceEvents { get; set; } = new List<MaintenanceEvent>();
        public List<PrinterError> Errors { get; set; } = new List<PrinterError>();
        public string Status { get; set; }
    }
}
