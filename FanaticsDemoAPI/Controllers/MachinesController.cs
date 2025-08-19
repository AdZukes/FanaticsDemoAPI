using FanaticsDemoAPI.Data;
using FanaticsDemoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public ActionResult<IEnumerable<OffsetPrinter>> MachineSummary()
        {

            //======================================
            //Requirement #3: View Machine Summary
            //======================================

            var offsetPrinters = _context.OffsetPrinters
            .Include(p => p.Statuses
            .OrderByDescending(p => p.Timestamp)
            .Take(1))
            .ToList();

            return Ok(offsetPrinters);

        }


        [HttpGet("{id}")]
        public ActionResult<OffsetPrinter> GetMachine(string id)
        {
            //var offsetPrinter = _context.OffsetPrinters.Where(p => p.PrinterId == id).FirstOrDefault();

            var offsetPrinter = _context.OffsetPrinters
                .Include(p => p.Statuses
                .OrderByDescending(p => p.Timestamp)
                .Take(5))
                .Where(p => p.PrinterId == id).FirstOrDefault();

            if (offsetPrinter == null)
            {
                return NotFound($"Machine with ID {id} not found.");
            }

            return Ok(offsetPrinter);

        }


        [HttpPost]
        public ActionResult<OffsetPrinter> RegisterMachine(AddPrinterDto AddPrinter) //Tested by JWR 08/19/2025
        {
            //======================================
            //Requirement #1: Register a new machine
            //======================================

            //This validation is not required, this is captured by the models [Required] Annotation, but it is a good practice to validate the input data.
            if (AddPrinter.MachineName == null || AddPrinter.MachineLocation == null || AddPrinter.MachineDescription == null)
            {
                return BadRequest("Invalid machine data. Please enter Machine Name, Machine Location and Machine Description");
            }

            var MachineList = _context.OffsetPrinters.ToList();

            OffsetPrinter newMachine = new OffsetPrinter();

            newMachine.Name = AddPrinter.MachineName.Trim();
            newMachine.Location = AddPrinter.MachineLocation.Trim();
            newMachine.Description = AddPrinter.MachineDescription.Trim();

            var existingMachine = MachineList.FirstOrDefault(p => p.Name == newMachine.Name.Trim());

            if (existingMachine != null)
            {
                return Conflict($"Machine with ID {newMachine.Name} already exists.");
            }


            int nextPrinterNumber = MachineList.Count() + 1;
            
            newMachine.PrinterId = $"PRN{nextPrinterNumber:D3}";


            //======================== The following Mock Data is for Fanatics Demo purposes only ==========================
            var rand = new Random();

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

            //====================== End Mock Data =================================================================


            newMachine.CreationDate = DateTime.Now;
            newMachine.LastUpdateDate = DateTime.Now;

            _context.OffsetPrinters.Add(newMachine);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetMachine), new { id = newMachine.PrinterId }, newMachine);
        }


        [HttpPost("{id}/status")]
        public ActionResult<OffsetPrinter> LogMachineStatus(string id, string printerStatus) //Tested by JWR 08/19/2025
        {
            //======================================
            //Requirement #2: Log Machine Status
            //======================================

            if (printerStatus == null)
            {
                return BadRequest("Invalid machine data. Please enter printerStatus, printerStatusTimestamp");
            }

            var offsetPrinter = _context.OffsetPrinters.Where(p => p.PrinterId == id.Trim()).FirstOrDefault();

            if (offsetPrinter == null)
            {
                return NotFound($"Machine with ID {id} not found.");
            }

            var PrinterStatusCount = _context.PrinterStatuses.Count(p => p.StatusId.StartsWith(id));

            PrinterStatus newStatus = new PrinterStatus
            {
                StatusId = $"{id}-{PrinterStatusCount + 1}",
                Message = printerStatus.Trim(),
                Timestamp = DateTime.Now
            };

            offsetPrinter.Statuses.Add(newStatus);
            _context.SaveChanges();

            return Ok(offsetPrinter);

        }




    }
}
