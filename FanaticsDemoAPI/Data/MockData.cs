using FanaticsDemoAPI.Models;

namespace FanaticsDemoAPI.Data
{
    public class MockData
    {
      
        public List<OffsetPrinter> GetOffsetPrinters()
        {
            List<OffsetPrinter> PrinterList = new List<OffsetPrinter>();
            var rand = new Random();

            for (int i = 1; i <= 10; i++)
            {
                PrinterList.Add(new OffsetPrinter
                {
                    PrinterId = $"PRN{i:D3}",
                    Name = $"PrinterModel{i}",
                    Description = "Offset printer for commercial use",
                    Model = $"Model-{i}",
                    Location = "Arlington, TX",
                    JobId = $"JOB{i:D3}",
                    JobName = $"PrintJob{i}",
                    JobStartTime = DateTime.Now.AddHours(-rand.Next(1, 48)),
                    JobEndTime = DateTime.Now,
                    TotalPagesPrinted = rand.Next(1000, 50000),
                    PaperType = rand.Next(0, 2) == 0 ? "Glossy" : "Matte",
                    InkType = rand.Next(0, 2) == 0 ? "CMYK" : "Black",
                    InkUsageLiters = Math.Round(rand.NextDouble() * 20, 2),
                    PaperWasteKg = Math.Round(rand.NextDouble() * 5, 2),
                    Downtime = TimeSpan.FromMinutes(rand.Next(0, 120)),
                    EnergyConsumptionKWh = Math.Round(rand.NextDouble() * 200, 2),
                    MaintenanceEvents = new List<MaintenanceEvent>
                    {
                        new MaintenanceEvent
                        {
                            EventId = $"ME{i:D3}",
                            Timestamp = DateTime.Now.AddDays(-rand.Next(1, 10)),
                            Description = "Routine check",
                            Technician = "Tech A"
                        }
                    },
                    Errors = new List<PrinterError>
                    {
                        new PrinterError
                        {
                            ErrorCodeId = $"E{i:D3}",
                            Message = "Minor error",
                            Timestamp = DateTime.Now.AddMinutes(-rand.Next(1, 60))
                        }
                    },
                    Statuses = new List<PrinterStatus>
                    {
                        new PrinterStatus
                        {
                            StatusId = $"PRN{i:D3}" + "-1",
                            Message = "running",
                            Timestamp = DateTime.Now.AddMinutes(-rand.Next(1, 60))
                        },
                        new PrinterStatus
                        {
                            StatusId = $"PRN{i:D3}" + "-2",
                            Message = "error",
                            Timestamp = DateTime.Now.AddMinutes(-rand.Next(1, 60))
                        },
                        new PrinterStatus
                        {
                            StatusId = $"PRN{i:D3}" + "-3",
                            Message = "idle",
                            Timestamp = DateTime.Now.AddMinutes(-rand.Next(1, 60))
                        },
                        new PrinterStatus
                        {
                            StatusId = $"PRN{i:D3}" + "-4",
                            Message = "maintenance",
                            Timestamp = DateTime.Now.AddMinutes(-rand.Next(1, 60))
                        },
                        new PrinterStatus
                        {
                            StatusId = $"PRN{i:D3}" + "-5",
                            Message = "running",
                            Timestamp = DateTime.Now.AddMinutes(-rand.Next(1, 60))
                        }

                    },
                    CreationDate = DateTime.Now.AddDays(-rand.Next(1, 30)),
                    LastUpdateDate = DateTime.Now.AddDays(-rand.Next(1, 30))

                });

            }

            return PrinterList;

        }
       
    }
}
