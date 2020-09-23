using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EstateReporting.Database.Migrations
{
    public partial class TransactionFeesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransactionFees",
                columns: table => new
                {
                    FeeId = table.Column<Guid>(nullable: false),
                    TransactionId = table.Column<Guid>(nullable: false),
                    CalculatedValue = table.Column<decimal>(nullable: false),
                    CalculationType = table.Column<int>(nullable: false),
                    EventId = table.Column<Guid>(nullable: false),
                    FeeType = table.Column<int>(nullable: false),
                    FeeValue = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionFees", x => new { x.TransactionId, x.FeeId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionFees");
        }
    }
}
