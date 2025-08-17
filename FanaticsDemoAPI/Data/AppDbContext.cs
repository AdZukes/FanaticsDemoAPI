using FanaticsDemoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FanaticsDemoAPI.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<OffsetPrinter> OffsetPrinters { get; set; }
        public DbSet<MaintenanceEvent> MaintenanceEvents { get; set; }
        public DbSet<PrinterError> PrinterErrors { get; set; }
    }
}
