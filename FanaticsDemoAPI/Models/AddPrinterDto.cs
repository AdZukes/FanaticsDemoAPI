using System.ComponentModel.DataAnnotations;

namespace FanaticsDemoAPI.Models
{
    public class AddPrinterDto
    {
        [Required]
        public string MachineName { get; set; }
        [Required]
        public string MachineLocation { get; set; }
        [Required]
        public string MachineDescription { get; set; }
    }
}
