using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EstateReporting.Database.Migrations.MySql
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
                    StatementId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EstateId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MerchantId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StatementCreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    StatementGeneratedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statementheader", x => x.StatementId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "statementline",
                columns: table => new
                {
                    StatementId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ActivityDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ActivityType = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EstateId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MerchantId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ActivityDescription = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    OutAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statementline", x => new { x.StatementId, x.TransactionId, x.ActivityDateTime, x.ActivityType });
                })
                .Annotation("MySql:CharSet", "utf8mb4");
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
