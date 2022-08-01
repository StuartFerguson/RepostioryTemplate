using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstateReporting.Database.Migrations.MySql
{
    public partial class AddTransactionSource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransactionSource",
                table: "transaction",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionSource",
                table: "transaction");
        }
    }
}
