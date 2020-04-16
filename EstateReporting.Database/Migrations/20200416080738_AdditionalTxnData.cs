using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EstateReporting.Database.Migrations
{
    public partial class AdditionalTxnData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "transactionadditionalrequestdata",
                columns: table => new
                {
                    EstateId = table.Column<Guid>(nullable: false),
                    MerchantId = table.Column<Guid>(nullable: false),
                    TransactionId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<string>(nullable: true),
                    CustomerAccountNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactionadditionalrequestdata", x => new { x.EstateId, x.MerchantId, x.TransactionId });
                });

            migrationBuilder.CreateTable(
                name: "transactionadditionalresponsedata",
                columns: table => new
                {
                    TransactionId = table.Column<Guid>(nullable: false),
                    EstateId = table.Column<Guid>(nullable: false),
                    MerchantId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactionadditionalresponsedata", x => x.TransactionId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transactionadditionalrequestdata");

            migrationBuilder.DropTable(
                name: "transactionadditionalresponsedata");
        }
    }
}
