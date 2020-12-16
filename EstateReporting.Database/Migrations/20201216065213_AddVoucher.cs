using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EstateReporting.Database.Migrations
{
    public partial class AddVoucher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "voucher",
                columns: table => new
                {
                    VoucherId = table.Column<Guid>(nullable: false),
                    EstateId = table.Column<Guid>(nullable: false),
                    ExpiryDate = table.Column<DateTime>(nullable: false),
                    IsGenerated = table.Column<bool>(nullable: false),
                    IsIssued = table.Column<bool>(nullable: false),
                    OperatorIdentifier = table.Column<string>(nullable: true),
                    RecipientEmail = table.Column<string>(nullable: true),
                    RecipientMobile = table.Column<string>(nullable: true),
                    Value = table.Column<decimal>(nullable: false),
                    VoucherCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voucher", x => x.VoucherId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "voucher");
        }
    }
}
