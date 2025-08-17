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

        public MachinesController(MockData mockData)
        {
            _mockData = mockData;

        }

        [HttpGet]
        public ActionResult<IEnumerable<OffsetPrinter>> GetMachines()
        {
            var offsetPrinters = _mockData.GetOffsetPrinters();

            return Ok(offsetPrinters);

        }

        [HttpGet("{id}")]
        public ActionResult<OffsetPrinter> GetMachine(string id)
        {
            var offsetPrinter = _mockData.GetOffsetPrinters().Where(p => p.PrinterId == id).FirstOrDefault();

            if (offsetPrinter == null)
            {
                return NotFound($"Machine with ID {id} not found.");
            }

            return Ok(offsetPrinter);

        }

        [HttpPost]
        public ActionResult<OffsetPrinter> CreateMachine(AddPrinterDto AddPrinterDto)
        {
            OffsetPrinter newMachine = new OffsetPrinter();

            if (AddPrinterDto.MachineName == null || AddPrinterDto.MachineLocation == null || AddPrinterDto.MachineDescription == null)
            {
                return BadRequest("Invalid machine data. Please enter MachineName, MachineLocation and MachineDescription");
            }

            newMachine.Name = AddPrinterDto.MachineName.Trim();
            newMachine.Location = AddPrinterDto.MachineLocation.Trim();
            newMachine.Description = AddPrinterDto.MachineDescription.Trim();

            var existingMachine = _mockData.GetOffsetPrinters().FirstOrDefault(p => p.Name == newMachine.Name);

            if (existingMachine != null)
            {
                return Conflict($"Machine with ID {newMachine.Name} already exists.");
            }

            _mockData.AddOffsetPrinter(newMachine);

            return CreatedAtAction(nameof(GetMachine), new { id = newMachine.PrinterId }, newMachine);
        }





    }
}
