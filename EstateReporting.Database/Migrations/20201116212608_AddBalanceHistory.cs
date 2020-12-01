using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EstateReporting.Database.Migrations
{
    public partial class AddBalanceHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "merchantbalancehistory",
                columns: table => new
                {
                    EventId = table.Column<Guid>(nullable: false),
                    EstateId = table.Column<Guid>(nullable: false),
                    MerchantId = table.Column<Guid>(nullable: false),
                    AvailableBalance = table.Column<decimal>(nullable: false),
                    Balance = table.Column<decimal>(nullable: false),
                    ChangeAmount = table.Column<decimal>(nullable: false),
                    Reference = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_merchantbalancehistory", x => x.EventId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "merchantbalancehistory");
        }
    }
}
