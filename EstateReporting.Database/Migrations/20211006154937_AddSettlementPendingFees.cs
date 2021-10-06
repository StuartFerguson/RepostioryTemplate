using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EstateReporting.Database.Migrations
{
    public partial class AddSettlementPendingFees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Settlements",
                table: "Settlements");

            migrationBuilder.DropColumn(
                name: "FeesAwaitingSettlement",
                table: "Settlements");

            migrationBuilder.DropColumn(
                name: "SettledFees",
                table: "Settlements");

            migrationBuilder.RenameTable(
                name: "Settlements",
                newName: "settlement");

            migrationBuilder.AddPrimaryKey(
                name: "PK_settlement",
                table: "settlement",
                columns: new[] { "EstateId", "SettlementId" });

            migrationBuilder.CreateTable(
                name: "merchantsettlementfees",
                columns: table => new
                {
                    EstateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SettlementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CalculatedValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FeeCalculatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FeeValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MerchantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsSettled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_merchantsettlementfees", x => new { x.EstateId, x.SettlementId, x.TransactionId, x.FeeId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "merchantsettlementfees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_settlement",
                table: "settlement");

            migrationBuilder.RenameTable(
                name: "settlement",
                newName: "Settlements");

            migrationBuilder.AddColumn<int>(
                name: "FeesAwaitingSettlement",
                table: "Settlements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SettledFees",
                table: "Settlements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Settlements",
                table: "Settlements",
                column: "SettlementId");
        }
    }
}
