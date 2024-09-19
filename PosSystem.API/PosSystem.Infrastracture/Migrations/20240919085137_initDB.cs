using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PosSystem.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class initDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateSequence(
                name: "BillNumber",
                schema: "dbo",
                startValue: 1000000L);

            migrationBuilder.CreateSequence<int>(
                name: "ClientNumber",
                schema: "dbo",
                startValue: 1000L);

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.ClientNumber"),
                    Phone = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    UnitId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.UnitId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Categories_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BillDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BillNumber = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.BillNumber"),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, computedColumnSql: "[TotalAmount] - [TotalDiscount]"),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DueAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, computedColumnSql: "[TotalAmount] - [TotalDiscount] - [PaidAmount]"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId");
                    table.ForeignKey(
                        name: "FK_Invoices_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BuyingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnitId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId");
                    table.ForeignKey(
                        name: "FK_Products_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "UnitId");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InvoiceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnitId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, computedColumnSql: "[Quantity] * [Price]")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "UnitId");
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientId", "Address", "Name", "Notes", "Number", "Phone" },
                values: new object[,]
                {
                    { "01b7952b-fde0-4d7c-a53e-e70999a469e6", "123 Main St", "John", null, 1, "1234567890" },
                    { "85593aa9-5f7e-4c67-95a8-bd4a6474e9d8", "456 Elm St", "Jane", null, 2, "0987654321" },
                    { "d9b29e24-d2e8-45a5-b3ae-9d3f4aedf8aa", "789 Oak St", "Jim", null, 3, "1112223333" }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "Name", "Notes" },
                values: new object[,]
                {
                    { "4e8fbcb7-6d6e-4d85-ad5c-2e160d59a217", "Tech Corp", null },
                    { "74a3c775-2340-4e03-95c6-eee183a6f070", "Retail LLC", null },
                    { "85f36626-6316-4051-92da-d05f54a3eeaa", "Biz Inc", null }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "UnitId", "Name", "Notes" },
                values: new object[,]
                {
                    { "22ba30c6-5d60-4d48-a17a-eb8e152797eb", "Pack", null },
                    { "6d33b20d-10e2-4cc1-9a8f-ed9095bf043e", "Piece", null },
                    { "fc92090b-105f-4923-854c-bd362bde7f8d", "Box", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "EndTime", "FirstName", "LastName", "Password", "Role", "StartTime", "UserName" },
                values: new object[,]
                {
                    { "e5fcfa12-a3d2-4fd7-abbe-ffc919409a6b", new TimeSpan(0, 17, 0, 0, 0), "John", "Doe", "$2a$11$CtJBer78S24mWh5qAfH6buKl2PB/q3wSiypg6YzasdXOJG/Hn9UJC", 0, new TimeSpan(0, 9, 0, 0, 0), "admin1" },
                    { "f246b47b-80c2-411b-b8fa-fa90f994403b", new TimeSpan(0, 18, 0, 0, 0), "Alice", "Johnson", "$2a$11$J0ZF5KzUPdVHEXcBLCNfjOfXzMC7joyHDbPuNqxltFrL44YRaGs7u", 1, new TimeSpan(0, 10, 0, 0, 0), "alice.johnson" },
                    { "f49141da-adf2-4fcd-a756-aeb96f346e72", new TimeSpan(0, 16, 0, 0, 0), "Jane", "Smith", "$2a$11$nrFhIDyJsyvWtnbXPqIzCO9dbhgOkjWtOzzjXgn6Cm8hHWaPw2q72", 1, new TimeSpan(0, 8, 0, 0, 0), "jane.smith" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CompanyId", "Name", "Notes" },
                values: new object[,]
                {
                    { "760bb6fe-1dec-4485-8781-ccd980bff780", "74a3c775-2340-4e03-95c6-eee183a6f070", "Clothing", null },
                    { "7e076ad1-ebaf-4803-83f1-68e9e995269b", "4e8fbcb7-6d6e-4d85-ad5c-2e160d59a217", "Electronics", null },
                    { "82e8bc6a-7107-42b3-8759-c90bf1b9da63", "85f36626-6316-4051-92da-d05f54a3eeaa", "Furniture", null }
                });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "Id", "BillDate", "BillNumber", "ClientId", "Date", "PaidAmount", "TotalAmount", "TotalDiscount", "UserId" },
                values: new object[,]
                {
                    { "1468aa44-79dc-4652-8a52-760be80e18fb", new DateTime(2024, 9, 19, 11, 51, 36, 391, DateTimeKind.Local).AddTicks(1444), 3L, "d9b29e24-d2e8-45a5-b3ae-9d3f4aedf8aa", new DateTime(2024, 9, 19, 11, 51, 36, 391, DateTimeKind.Local).AddTicks(1454), 20m, 20m, 0m, "f246b47b-80c2-411b-b8fa-fa90f994403b" },
                    { "35b31972-8417-46ff-910c-a7319c42fd6f", new DateTime(2024, 9, 19, 11, 51, 36, 391, DateTimeKind.Local).AddTicks(1433), 2L, "85593aa9-5f7e-4c67-95a8-bd4a6474e9d8", new DateTime(2024, 9, 19, 11, 51, 36, 391, DateTimeKind.Local).AddTicks(1438), 50m, 50m, 0m, "f49141da-adf2-4fcd-a756-aeb96f346e72" },
                    { "7a731996-b5f2-4ff1-b56c-125f1657b9b0", new DateTime(2024, 9, 19, 11, 51, 36, 391, DateTimeKind.Local).AddTicks(1330), 1L, "01b7952b-fde0-4d7c-a53e-e70999a469e6", new DateTime(2024, 9, 19, 11, 51, 36, 391, DateTimeKind.Local).AddTicks(1422), 1000m, 1000m, 0m, "e5fcfa12-a3d2-4fd7-abbe-ffc919409a6b" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "BuyingPrice", "CategoryId", "CompanyId", "Name", "Notes", "Quantity", "SellingPrice", "UnitId" },
                values: new object[,]
                {
                    { "059ace", 10m, "760bb6fe-1dec-4485-8781-ccd980bff780", "74a3c775-2340-4e03-95c6-eee183a6f070", "T-Shirt", null, 200m, 20m, "22ba30c6-5d60-4d48-a17a-eb8e152797eb" },
                    { "41da07", 30m, "82e8bc6a-7107-42b3-8759-c90bf1b9da63", "85f36626-6316-4051-92da-d05f54a3eeaa", "Chair", null, 100m, 50m, "fc92090b-105f-4923-854c-bd362bde7f8d" },
                    { "e89ff6", 800m, "7e076ad1-ebaf-4803-83f1-68e9e995269b", "4e8fbcb7-6d6e-4d85-ad5c-2e160d59a217", "Laptop", null, 10m, 1000m, "6d33b20d-10e2-4cc1-9a8f-ed9095bf043e" }
                });

            migrationBuilder.InsertData(
                table: "InvoiceItems",
                columns: new[] { "Id", "InvoiceId", "Price", "ProductId", "Quantity", "UnitId" },
                values: new object[,]
                {
                    { "80e1fba2-5763-4398-896b-36191256c59d", "1468aa44-79dc-4652-8a52-760be80e18fb", 20m, "059ace", 1m, "22ba30c6-5d60-4d48-a17a-eb8e152797eb" },
                    { "a6a3537f-2148-4821-9af4-ee363c3fd59c", "7a731996-b5f2-4ff1-b56c-125f1657b9b0", 1000m, "e89ff6", 1m, "6d33b20d-10e2-4cc1-9a8f-ed9095bf043e" },
                    { "e7fca972-6014-4536-b01b-dd87b1474ce7", "35b31972-8417-46ff-910c-a7319c42fd6f", 50m, "41da07", 1m, "fc92090b-105f-4923-854c-bd362bde7f8d" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CompanyId",
                table: "Categories",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Address",
                table: "Clients",
                column: "Address");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Name",
                table: "Clients",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Number",
                table: "Clients",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Phone",
                table: "Clients",
                column: "Phone");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Name",
                table: "Companies",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceId",
                table: "InvoiceItems",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_ProductId",
                table: "InvoiceItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_UnitId",
                table: "InvoiceItems",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_BillDate",
                table: "Invoices",
                column: "BillDate");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_BillNumber",
                table: "Invoices",
                column: "BillNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ClientId",
                table: "Invoices",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_Date",
                table: "Invoices",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_UserId",
                table: "Invoices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CompanyId",
                table: "Products",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitId",
                table: "Products",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_Name",
                table: "Units",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_FirstName",
                table: "Users",
                column: "FirstName");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LastName",
                table: "Users",
                column: "LastName");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropSequence(
                name: "BillNumber",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "ClientNumber",
                schema: "dbo");
        }
    }
}
