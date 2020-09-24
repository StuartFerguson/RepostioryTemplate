using Microsoft.EntityFrameworkCore.Migrations;

namespace EstateReporting.Database.Migrations
{
    public partial class fixtransactionfee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionFees",
                table: "TransactionFees");

            migrationBuilder.RenameTable(
                name: "TransactionFees",
                newName: "transactionfee");

            migrationBuilder.AddPrimaryKey(
                name: "PK_transactionfee",
                table: "transactionfee",
                columns: new[] { "TransactionId", "FeeId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_transactionfee",
                table: "transactionfee");

            migrationBuilder.RenameTable(
                name: "transactionfee",
                newName: "TransactionFees");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionFees",
                table: "TransactionFees",
                columns: new[] { "TransactionId", "FeeId" });
        }
    }
}
