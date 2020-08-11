using Microsoft.EntityFrameworkCore.Migrations;

namespace EstateReporting.Database.Migrations
{
    public partial class transactionfeeupdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FeeType",
                table: "contractproducttransactionfee",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "contractproducttransactionfee",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeeType",
                table: "contractproducttransactionfee");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "contractproducttransactionfee");
        }
    }
}
