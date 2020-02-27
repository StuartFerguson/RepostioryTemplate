using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EstateReporting.Database.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "estate",
                columns: table => new
                {
                    EstateId = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estate", x => x.EstateId);
                });

            migrationBuilder.CreateTable(
                name: "estatesecurityuser",
                columns: table => new
                {
                    EstateId = table.Column<Guid>(nullable: false),
                    SecurityUserId = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    EmailAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estatesecurityuser", x => new { x.SecurityUserId, x.EstateId });
                });

            migrationBuilder.CreateTable(
                name: "merchant",
                columns: table => new
                {
                    EstateId = table.Column<Guid>(nullable: false),
                    MerchantId = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_merchant", x => new { x.EstateId, x.MerchantId });
                });

            migrationBuilder.CreateTable(
                name: "merchantaddress",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(nullable: false),
                    EstateId = table.Column<Guid>(nullable: false),
                    MerchantId = table.Column<Guid>(nullable: false),
                    AddressLine1 = table.Column<string>(nullable: true),
                    AddressLine2 = table.Column<string>(nullable: true),
                    AddressLine3 = table.Column<string>(nullable: true),
                    AddressLine4 = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    PostalCode = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: true),
                    Town = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_merchantaddress", x => new { x.EstateId, x.MerchantId, x.AddressId });
                });

            migrationBuilder.CreateTable(
                name: "merchantcontact",
                columns: table => new
                {
                    ContactId = table.Column<Guid>(nullable: false),
                    EstateId = table.Column<Guid>(nullable: false),
                    MerchantId = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    EmailAddress = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_merchantcontact", x => new { x.EstateId, x.MerchantId, x.ContactId });
                });

            migrationBuilder.CreateTable(
                name: "merchantdevice",
                columns: table => new
                {
                    DeviceId = table.Column<Guid>(nullable: false),
                    EstateId = table.Column<Guid>(nullable: false),
                    MerchantId = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    DeviceIdentifier = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_merchantdevice", x => new { x.EstateId, x.MerchantId, x.DeviceId });
                });

            migrationBuilder.CreateTable(
                name: "merchantsecurityuser",
                columns: table => new
                {
                    EstateId = table.Column<Guid>(nullable: false),
                    MerchantId = table.Column<Guid>(nullable: false),
                    SecurityUserId = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    EmailAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_merchantsecurityuser", x => new { x.EstateId, x.MerchantId, x.SecurityUserId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "estate");

            migrationBuilder.DropTable(
                name: "estatesecurityuser");

            migrationBuilder.DropTable(
                name: "merchant");

            migrationBuilder.DropTable(
                name: "merchantaddress");

            migrationBuilder.DropTable(
                name: "merchantcontact");

            migrationBuilder.DropTable(
                name: "merchantdevice");

            migrationBuilder.DropTable(
                name: "merchantsecurityuser");
        }
    }
}
