using FanaticsDemoAPI.Data;
using FanaticsDemoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FanaticsDemoAPI.Controllers
{
    [Route("api/machines")]
    [ApiController]
    public class MachinesController : ControllerBase
    {
        private MockData _mockData;
        private ApplicationDbContext _context;

        public MachinesController(MockData mockData, ApplicationDbContext context)
        {
            _mockData = mockData;
            _context = context;
            InitializeDatabase(); // Ensure the database is initialized with mock data
        }

        private void InitializeDatabase()
        {
            if (!_context.OffsetPrinters.Any())
            {
                _context.OffsetPrinters.AddRange(_mockData.GetOffsetPrinters());
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<OffsetPrinter>> GetMachines()
        {
            var offsetPrinters = _context.OffsetPrinters.ToList();

            return Ok(offsetPrinters);

        }

        [HttpGet("{id}")]
        public ActionResult<OffsetPrinter> GetMachine(string id)
        {
            var offsetPrinter = _context.OffsetPrinters.Where(p => p.PrinterId == id).FirstOrDefault();

            if (offsetPrinter == null)
            {
                return NotFound($"Machine with ID {id} not found.");
            }

            return Ok(offsetPrinter);

        }


        [HttpPost]
        public ActionResult<OffsetPrinter> CreateMachine(AddPrinterDto AddPrinterDto)
        {
            if (AddPrinterDto.MachineName == null || AddPrinterDto.MachineLocation == null || AddPrinterDto.MachineDescription == null)
            {
                return BadRequest("Invalid machine data. Please enter MachineName, MachineLocation and MachineDescription");
            }

            var MachineList = _context.OffsetPrinters.ToList();

            OffsetPrinter newMachine = new OffsetPrinter();

            newMachine.Name = AddPrinterDto.MachineName.Trim();
            newMachine.Location = AddPrinterDto.MachineLocation.Trim();
            newMachine.Description = AddPrinterDto.MachineDescription.Trim();

            var existingMachine = MachineList.FirstOrDefault(p => p.Name == newMachine.Name);

            if (existingMachine != null)
            {
                return Conflict($"Machine with ID {newMachine.Name} already exists.");
            }


            int nextPrinterNumber = MachineList.Count() + 1;
            var rand = new Random();


            newMachine.PrinterId = $"PRN{nextPrinterNumber:D3}";

            newMachine.Model = $"Model-{nextPrinterNumber}";

            newMachine.JobId = $"JOB{nextPrinterNumber:D3}";
            newMachine.JobName = $"PrintJob{nextPrinterNumber}";
            newMachine.JobStartTime = DateTime.Now.AddHours(-rand.Next(1, 48));
            newMachine.JobEndTime = DateTime.Now;
            newMachine.TotalPagesPrinted = rand.Next(1000, 50000);
            newMachine.PaperType = rand.Next(0, 2) == 0 ? "Glossy" : "Matte";
            newMachine.InkType = rand.Next(0, 2) == 0 ? "CMYK" : "Black";
            newMachine.InkUsageLiters = Math.Round(rand.NextDouble() * 20, 2);
            newMachine.PaperWasteKg = Math.Round(rand.NextDouble() * 5, 2);
            newMachine.Downtime = TimeSpan.FromMinutes(rand.Next(0, 120));
            newMachine.EnergyConsumptionKWh = Math.Round(rand.NextDouble() * 200, 2);

            newMachine.MaintenanceEvents = new List<MaintenanceEvent>
            {
                new MaintenanceEvent
                {
                    EventId = $"ME{nextPrinterNumber:D3}",
                    Timestamp = DateTime.Now.AddDays(-rand.Next(1, 10)),
                    Description = "Routine check",
                    Technician = "Tech A"
                }
            };

            newMachine.Errors = new List<PrinterError>
            {
                new PrinterError
                {
                    ErrorCodeId = $"E{nextPrinterNumber:D3}",
                    Message = "Minor error",
                    Timestamp = DateTime.Now.AddMinutes(-rand.Next(1, 60))
                }
            };

            newMachine.Statuses = new List<PrinterStatus>
            {
                new PrinterStatus
                {
                    StatusId = $"S{nextPrinterNumber:D3}",
                    Message = "running",
                    Timestamp = DateTime.Now.AddMinutes(-rand.Next(1, 60))
                },
                new PrinterStatus
                {
                    StatusId = $"S{nextPrinterNumber:D3}",
                    Message = "error",
                    Timestamp = DateTime.Now.AddMinutes(-rand.Next(1, 60))
                },
                new PrinterStatus
                {
                    StatusId = $"S{nextPrinterNumber:D3}",
                    Message = "idle",
                    Timestamp = DateTime.Now.AddMinutes(-rand.Next(1, 60))
                },
                new PrinterStatus
                {
                    StatusId = $"S{nextPrinterNumber:D3}",
                    Message = "maintenance",
                    Timestamp = DateTime.Now.AddMinutes(-rand.Next(1, 60))
                },
                new PrinterStatus
                {
                    StatusId = $"S{nextPrinterNumber:D3}",
                    Message = "running",
                    Timestamp = DateTime.Now.AddMinutes(-rand.Next(1, 60))
                }

            };



            _context.OffsetPrinters.Add(newMachine);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetMachine), new { id = newMachine.PrinterId }, newMachine);
        }

        [HttpPost("{id}/status")]
        public ActionResult<OffsetPrinter> LogMachineStatus(string id, string printerstatus, DateTime printerstatustimestamp)
        {
            var offsetPrinter = _context.OffsetPrinters.Where(p => p.PrinterId == id).FirstOrDefault();

            if (offsetPrinter == null)
            {
                return NotFound($"Machine with ID {id} not found.");
            }

            //offsetPrinter.Status = printerstatus.Trim(); // Simulate a status update
            //offsetPrinter.StatusTimestamp = printerstatustimestamp;
            _context.SaveChanges();

            return Ok(offsetPrinter);

        }

        [HttpGet("{id}/getstatus")]
        public ActionResult<OffsetPrinter> LogMachineStatus(string id)
        {
            var offsetPrinter = _context.OffsetPrinters.Where(p => p.PrinterId == id).FirstOrDefault();

            if (offsetPrinter == null)
            {
                return NotFound($"Machine with ID {id} not found.");
            }

            PrinterStatusDto printerStatus = new PrinterStatusDto
            {
                PrinterId = offsetPrinter.PrinterId,
                MachineName = offsetPrinter.Name,
                //Status = offsetPrinter.Status
            };

            return Ok(printerStatus);

        }


    }
}
