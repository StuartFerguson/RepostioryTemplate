using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EstateReporting.Database.Migrations.SqlServer
{
    public partial class LoadStatementEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_merchantsettlementfees",
                table: "merchantsettlementfees");

            migrationBuilder.RenameTable(
                name: "merchantsettlementfees",
                newName: "merchantsettlementfee");

            migrationBuilder.AddPrimaryKey(
                name: "PK_merchantsettlementfee",
                table: "merchantsettlementfee",
                columns: new[] { "EstateId", "SettlementId", "TransactionId", "FeeId" });

            migrationBuilder.CreateTable(
                name: "statementheader",
                columns: table => new
                {
                    StatementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MerchantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatementCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatementGeneratedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statementheader", x => x.StatementId);
                });

            migrationBuilder.CreateTable(
                name: "statementline",
                columns: table => new
                {
                    StatementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivityDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActivityType = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MerchantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivityDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OutAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statementline", x => new { x.StatementId, x.TransactionId, x.ActivityDateTime, x.ActivityType });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "statementheader");

            migrationBuilder.DropTable(
                name: "statementline");

            migrationBuilder.DropPrimaryKey(
                name: "PK_merchantsettlementfee",
                table: "merchantsettlementfee");

            migrationBuilder.RenameTable(
                name: "merchantsettlementfee",
                newName: "merchantsettlementfees");

            migrationBuilder.AddPrimaryKey(
                name: "PK_merchantsettlementfees",
                table: "merchantsettlementfees",
                columns: new[] { "EstateId", "SettlementId", "TransactionId", "FeeId" });
        }
    }
}
