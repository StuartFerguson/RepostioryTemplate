using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EstateReporting.Database.Migrations
{
    public partial class storereconciliations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reconciliation",
                columns: table => new
                {
                    TransactionId = table.Column<Guid>(nullable: false),
                    DeviceIdentifier = table.Column<string>(nullable: true),
                    EstateId = table.Column<Guid>(nullable: false),
                    IsAuthorised = table.Column<bool>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false),
                    MerchantId = table.Column<Guid>(nullable: false),
                    ResponseCode = table.Column<string>(nullable: true),
                    ResponseMessage = table.Column<string>(nullable: true),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    TransactionDateTime = table.Column<DateTime>(nullable: false),
                    TransactionTime = table.Column<TimeSpan>(nullable: false),
                    TransactionCount = table.Column<int>(nullable: false),
                    TransactionValue = table.Column<decimal>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reconciliation", x => x.TransactionId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reconciliation");
        }
    }
}
