using FanaticsDemoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FanaticsDemoAPI.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<OffsetPrinter> OffsetPrinters { get; set; }
        public DbSet<MaintenanceEvent> MaintenanceEvents { get; set; }
        public DbSet<PrinterError> PrinterErrors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional model configuration can go here
        }
    }
}
