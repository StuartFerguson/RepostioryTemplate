using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EstateReporting.Database.Migrations
{
    public partial class OperatorDataAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "estateoperator",
                columns: table => new
                {
                    EstateId = table.Column<Guid>(nullable: false),
                    OperatorId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    RequireCustomMerchantNumber = table.Column<bool>(nullable: false),
                    RequireCustomTerminalNumber = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estateoperator", x => new { x.EstateId, x.OperatorId });
                });

            migrationBuilder.CreateTable(
                name: "merchantoperator",
                columns: table => new
                {
                    EstateId = table.Column<Guid>(nullable: false),
                    MerchantId = table.Column<Guid>(nullable: false),
                    OperatorId = table.Column<Guid>(nullable: false),
                    MerchantNumber = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TerminalNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_merchantoperator", x => new { x.EstateId, x.MerchantId, x.OperatorId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "estateoperator");

            migrationBuilder.DropTable(
                name: "merchantoperator");
        }
    }
}
