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
                    Quantity = table.Column<int>(type: "int", nullable: false),
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
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, computedColumnSql: "[Quantity] * [Price]"),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, computedColumnSql: "([Quantity] * [Price]) - [Discount]")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "UnitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientId", "Address", "Name", "Notes", "Number", "Phone" },
                values: new object[,]
                {
                    { "7eb580b7-fd3a-49fb-a8e2-364d1e9d8482", "789 Oak St", "Jim", null, 3, "1112223333" },
                    { "da314426-bcd2-492f-a2b5-39628a6f202d", "123 Main St", "John", null, 1, "1234567890" },
                    { "eb1a6e87-657b-4c21-905e-e345f4bd2fff", "456 Elm St", "Jane", null, 2, "0987654321" }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "Name", "Notes" },
                values: new object[,]
                {
                    { "34111bfb-255b-4346-9d36-3beb91fa1c27", "Biz Inc", null },
                    { "6e5f48e3-408c-480a-9833-31475d0a481d", "Retail LLC", null },
                    { "e174e132-9e37-407b-9e85-8747e90d7f53", "Tech Corp", null }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "UnitId", "Name", "Notes" },
                values: new object[,]
                {
                    { "1b3ed427-373b-431e-829b-cb46a619f80e", "Pack", null },
                    { "9d5b276a-5c24-4672-9ed9-34cfe5cca9ea", "Piece", null },
                    { "a099b55b-0330-423a-b2e3-90bc388dfe99", "Box", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "EndTime", "FirstName", "LastName", "Password", "Role", "StartTime", "UserName" },
                values: new object[,]
                {
                    { "baca97ee-f2da-4e63-be4b-54a004200d2a", new TimeSpan(0, 17, 0, 0, 0), "John", "Doe", "$2a$11$g3VBtLqHZA6NFfOn7BpQ8euXt0Bu/s0IMfE77MI3NtN1OFjWAxKJS", 0, new TimeSpan(0, 9, 0, 0, 0), "admin1" },
                    { "bf3d2e89-6ba6-4794-b6b7-7f9ee3ca6cfe", new TimeSpan(0, 18, 0, 0, 0), "Alice", "Johnson", "$2a$11$OsEooJZF2IEBBMFSnMdCVu9e.fakMT0UA365OyWKR.Gr9pdcEpCYK", 1, new TimeSpan(0, 10, 0, 0, 0), "alice.johnson" },
                    { "cc655fe4-ba86-48a2-aa62-05d454a24b4d", new TimeSpan(0, 16, 0, 0, 0), "Jane", "Smith", "$2a$11$D7cX7XTrWyMydVBMZ3wvj.0j.ZYqJ/UKO8R1qTe6UkDcpUthPFugu", 1, new TimeSpan(0, 8, 0, 0, 0), "jane.smith" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CompanyId", "Name", "Notes" },
                values: new object[,]
                {
                    { "1792c9c9-285e-439a-bc94-97e3d6c64dca", "34111bfb-255b-4346-9d36-3beb91fa1c27", "Furniture", null },
                    { "5ee2e819-a35e-4356-870a-40f3dab99a96", "6e5f48e3-408c-480a-9833-31475d0a481d", "Clothing", null },
                    { "d2c7bc49-90b4-47c8-9ed6-df23c974f284", "e174e132-9e37-407b-9e85-8747e90d7f53", "Electronics", null }
                });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "Id", "BillDate", "BillNumber", "ClientId", "Date", "PaidAmount", "TotalAmount", "TotalDiscount", "UserId" },
                values: new object[,]
                {
                    { "013e9469-9f4a-4b07-9d00-d21d44afc4b3", new DateTime(2024, 9, 17, 6, 1, 19, 480, DateTimeKind.Local).AddTicks(2230), 2L, "eb1a6e87-657b-4c21-905e-e345f4bd2fff", new DateTime(2024, 9, 17, 6, 1, 19, 480, DateTimeKind.Local).AddTicks(2238), 50m, 50m, 0m, "cc655fe4-ba86-48a2-aa62-05d454a24b4d" },
                    { "5fb504a4-2ee5-4a4c-a01c-bfe997cadb16", new DateTime(2024, 9, 17, 6, 1, 19, 480, DateTimeKind.Local).AddTicks(2245), 3L, "7eb580b7-fd3a-49fb-a8e2-364d1e9d8482", new DateTime(2024, 9, 17, 6, 1, 19, 480, DateTimeKind.Local).AddTicks(2592), 20m, 20m, 0m, "bf3d2e89-6ba6-4794-b6b7-7f9ee3ca6cfe" },
                    { "a6be97f4-31ff-4c6a-8740-902351064c4f", new DateTime(2024, 9, 17, 6, 1, 19, 480, DateTimeKind.Local).AddTicks(2154), 1L, "da314426-bcd2-492f-a2b5-39628a6f202d", new DateTime(2024, 9, 17, 6, 1, 19, 480, DateTimeKind.Local).AddTicks(2224), 1000m, 1000m, 0m, "baca97ee-f2da-4e63-be4b-54a004200d2a" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "BuyingPrice", "CategoryId", "CompanyId", "Name", "Notes", "Quantity", "SellingPrice", "UnitId" },
                values: new object[,]
                {
                    { "2c1167", 10m, "5ee2e819-a35e-4356-870a-40f3dab99a96", "6e5f48e3-408c-480a-9833-31475d0a481d", "T-Shirt", null, 200, 20m, "1b3ed427-373b-431e-829b-cb46a619f80e" },
                    { "cefb5e", 800m, "d2c7bc49-90b4-47c8-9ed6-df23c974f284", "e174e132-9e37-407b-9e85-8747e90d7f53", "Laptop", null, 10, 1000m, "9d5b276a-5c24-4672-9ed9-34cfe5cca9ea" },
                    { "e19ce4", 30m, "1792c9c9-285e-439a-bc94-97e3d6c64dca", "34111bfb-255b-4346-9d36-3beb91fa1c27", "Chair", null, 100, 50m, "a099b55b-0330-423a-b2e3-90bc388dfe99" }
                });

            migrationBuilder.InsertData(
                table: "InvoiceItems",
                columns: new[] { "Id", "Discount", "InvoiceId", "Price", "ProductId", "Quantity", "UnitId" },
                values: new object[,]
                {
                    { "6d72b613-e248-47e6-8273-761870949448", 0m, "a6be97f4-31ff-4c6a-8740-902351064c4f", 1000m, "cefb5e", 1m, "9d5b276a-5c24-4672-9ed9-34cfe5cca9ea" },
                    { "93bbbf8a-c437-48b3-bfd1-a98274871455", 0m, "013e9469-9f4a-4b07-9d00-d21d44afc4b3", 50m, "e19ce4", 1m, "a099b55b-0330-423a-b2e3-90bc388dfe99" },
                    { "dfab5abf-665c-43dc-82e8-2c8a44643fb6", 0m, "5fb504a4-2ee5-4a4c-a01c-bfe997cadb16", 20m, "2c1167", 1m, "1b3ed427-373b-431e-829b-cb46a619f80e" }
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
                column: "CategoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CompanyId",
                table: "Products",
                column: "CompanyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitId",
                table: "Products",
                column: "UnitId",
                unique: true);

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
