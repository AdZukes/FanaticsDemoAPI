using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FanaticsDemoAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OffsetPrinters",
                columns: table => new
                {
                    PrinterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JobEndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalPagesPrinted = table.Column<int>(type: "int", nullable: false),
                    PaperType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InkType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InkUsageLiters = table.Column<double>(type: "float", nullable: false),
                    PaperWasteKg = table.Column<double>(type: "float", nullable: false),
                    Downtime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EnergyConsumptionKWh = table.Column<double>(type: "float", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OffsetPrinters", x => x.PrinterId);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceEvents",
                columns: table => new
                {
                    EventId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Technician = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OffsetPrinterPrinterId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceEvents", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_MaintenanceEvents_OffsetPrinters_OffsetPrinterPrinterId",
                        column: x => x.OffsetPrinterPrinterId,
                        principalTable: "OffsetPrinters",
                        principalColumn: "PrinterId");
                });

            migrationBuilder.CreateTable(
                name: "PrinterErrors",
                columns: table => new
                {
                    ErrorCodeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OffsetPrinterPrinterId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrinterErrors", x => x.ErrorCodeId);
                    table.ForeignKey(
                        name: "FK_PrinterErrors_OffsetPrinters_OffsetPrinterPrinterId",
                        column: x => x.OffsetPrinterPrinterId,
                        principalTable: "OffsetPrinters",
                        principalColumn: "PrinterId");
                });

            migrationBuilder.CreateTable(
                name: "PrinterStatuses",
                columns: table => new
                {
                    StatusId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OffsetPrinterPrinterId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrinterStatuses", x => x.StatusId);
                    table.ForeignKey(
                        name: "FK_PrinterStatuses_OffsetPrinters_OffsetPrinterPrinterId",
                        column: x => x.OffsetPrinterPrinterId,
                        principalTable: "OffsetPrinters",
                        principalColumn: "PrinterId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceEvents_OffsetPrinterPrinterId",
                table: "MaintenanceEvents",
                column: "OffsetPrinterPrinterId");

            migrationBuilder.CreateIndex(
                name: "IX_PrinterErrors_OffsetPrinterPrinterId",
                table: "PrinterErrors",
                column: "OffsetPrinterPrinterId");

            migrationBuilder.CreateIndex(
                name: "IX_PrinterStatuses_OffsetPrinterPrinterId",
                table: "PrinterStatuses",
                column: "OffsetPrinterPrinterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaintenanceEvents");

            migrationBuilder.DropTable(
                name: "PrinterErrors");

            migrationBuilder.DropTable(
                name: "PrinterStatuses");

            migrationBuilder.DropTable(
                name: "OffsetPrinters");
        }
    }
}
