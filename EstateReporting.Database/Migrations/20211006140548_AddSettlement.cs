using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EstateReporting.Database.Migrations
{
    public partial class AddSettlement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settlements",
                columns: table => new
                {
                    SettlementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SettlementDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    FeesAwaitingSettlement = table.Column<int>(type: "int", nullable: false),
                    SettledFees = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settlements", x => x.SettlementId);
                });

            migrationBuilder.CreateTable(
                name: "uvwFileImportLogView",
                columns: table => new
                {
                    FileImportLogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImportLogDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImportLogDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImportLogTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    FileCount = table.Column<int>(type: "int", nullable: false),
                    MerchantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "uvwFileView",
                columns: table => new
                {
                    FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileReceivedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileReceivedTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    MerchantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MerchantName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LineCount = table.Column<int>(type: "int", nullable: false),
                    PendingCount = table.Column<int>(type: "int", nullable: false),
                    FailedCount = table.Column<int>(type: "int", nullable: false),
                    SuccessCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settlements");

            migrationBuilder.DropTable(
                name: "uvwFileImportLogView");

            migrationBuilder.DropTable(
                name: "uvwFileView");
        }
    }
}
