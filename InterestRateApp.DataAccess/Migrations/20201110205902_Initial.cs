using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InterestRateApp.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PersonalId = table.Column<string>(maxLength: 11, nullable: true),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Agreements",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    Margin = table.Column<decimal>(nullable: false),
                    BaseRateCode = table.Column<string>(maxLength: 9, nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agreements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agreements_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "FirstName", "LastName", "PersonalId" },
                values: new object[] { new Guid("c7e7d3b9-1f25-4609-bbec-2fa1b7564561"), "Peter", "Peterson", "12345678901" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "FirstName", "LastName", "PersonalId" },
                values: new object[] { new Guid("f5e43d8a-16e8-4cef-ba88-0ae3288aa7f7"), "Robert", "Robertson", "01987654321" });

            migrationBuilder.InsertData(
                table: "Agreements",
                columns: new[] { "Id", "Amount", "BaseRateCode", "CustomerId", "Duration", "Margin" },
                values: new object[] { new Guid("7c25e824-2582-497c-8c92-0d39da9f74bf"), 12000, "VILIBOR1m", new Guid("c7e7d3b9-1f25-4609-bbec-2fa1b7564561"), 60, 1.6m });

            migrationBuilder.InsertData(
                table: "Agreements",
                columns: new[] { "Id", "Amount", "BaseRateCode", "CustomerId", "Duration", "Margin" },
                values: new object[] { new Guid("a686e94a-81da-4990-8a83-caa45358896d"), 8000, "VILIBOR3m", new Guid("f5e43d8a-16e8-4cef-ba88-0ae3288aa7f7"), 36, 2.2m });

            migrationBuilder.InsertData(
                table: "Agreements",
                columns: new[] { "Id", "Amount", "BaseRateCode", "CustomerId", "Duration", "Margin" },
                values: new object[] { new Guid("e3f20c0f-a742-4684-ace1-da3470c4c990"), 8000, "VILIBOR1y", new Guid("f5e43d8a-16e8-4cef-ba88-0ae3288aa7f7"), 24, 1.85m });

            migrationBuilder.CreateIndex(
                name: "IX_Agreements_CustomerId",
                table: "Agreements",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PersonalId",
                table: "Customers",
                column: "PersonalId",
                unique: true,
                filter: "[PersonalId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agreements");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
