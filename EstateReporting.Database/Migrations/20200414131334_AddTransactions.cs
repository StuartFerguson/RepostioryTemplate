using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EstateReporting.Database.Migrations
{
    public partial class AddTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                                         name: "transaction",
                                         columns: table => new
                                                           {
                                                               TransactionId = table.Column<Guid>(nullable: false),
                                                               EstateId = table.Column<Guid>(nullable: false),
                                                               MerchantId = table.Column<Guid>(nullable: false),
                                                               DeviceIdentifier = table.Column<string>(nullable: true),
                                                               AuthorisationCode = table.Column<string>(nullable: true),
                                                               IsAuthorised = table.Column<bool>(nullable: false),
                                                               IsCompleted = table.Column<bool>(nullable: false),
                                                               ResponseCode = table.Column<string>(nullable: true),
                                                               ResponseMessage = table.Column<string>(nullable: true),
                                                               TransactionDate = table.Column<DateTime>(nullable: false),
                                                               TransactionTime = table.Column<TimeSpan>(nullable: false),
                                                               TransactionDateTime = table.Column<DateTime>(nullable: false),
                                                               TransactionNumber = table.Column<string>(nullable: true),
                                                               TransactionReference = table.Column<string>(nullable: true),
                                                               TransactionType = table.Column<string>(nullable: true)
                                                           },
                                         constraints: table =>
                                                      {
                                                          table.PrimaryKey("PK_transaction", x => new { x.EstateId, x.MerchantId, x.TransactionId });
                                                      });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transaction");
            
        }
    }
}
