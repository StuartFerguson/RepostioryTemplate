using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EstateReporting.Database.Migrations
{
    public partial class addcontractdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contract",
                columns: table => new
                {
                    ContractId = table.Column<Guid>(nullable: false),
                    EstateId = table.Column<Guid>(nullable: false),
                    OperatorId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contract", x => new { x.EstateId, x.OperatorId, x.ContractId });
                });

            migrationBuilder.CreateTable(
                name: "contractproduct",
                columns: table => new
                {
                    ContractId = table.Column<Guid>(nullable: false),
                    EstateId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    DisplayText = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    Value = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contractproduct", x => new { x.EstateId, x.ContractId, x.ProductId });
                });

            migrationBuilder.CreateTable(
                name: "contractproducttransactionfee",
                columns: table => new
                {
                    ContractId = table.Column<Guid>(nullable: false),
                    EstateId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    TransactionFeeId = table.Column<Guid>(nullable: false),
                    CalculationType = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Value = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contractproducttransactionfee", x => new { x.EstateId, x.ContractId, x.ProductId, x.TransactionFeeId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contract");

            migrationBuilder.DropTable(
                name: "contractproduct");

            migrationBuilder.DropTable(
                name: "contractproducttransactionfee");

        }
    }
}
