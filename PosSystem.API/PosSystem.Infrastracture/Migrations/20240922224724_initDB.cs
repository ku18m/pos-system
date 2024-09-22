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

            migrationBuilder.CreateSequence<int>(
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
                    BillNumber = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.BillNumber"),
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
                columns: new[] { "ClientId", "Address", "Name", "Notes", "Phone" },
                values: new object[,]
                {
                    { "2fa4aa37-e828-43a0-9654-09195e1fd031", "789 Oak St", "Jim", null, "1112223333" },
                    { "5abff7b1-5c8f-4228-a132-2e0487050ca5", "123 Main St", "John", null, "1234567890" },
                    { "b37b4bde-c4de-4bf8-bd10-961e185e045f", "456 Elm St", "Jane", null, "0987654321" }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "Name", "Notes" },
                values: new object[,]
                {
                    { "34e7fd45-27b4-44e6-b4d7-a8bdf9b979ff", "Tech Corp", null },
                    { "d36cc123-8515-4cec-afc2-65ad7a315578", "Retail LLC", null },
                    { "f39227bf-be19-43b8-9cfd-6bce63404190", "Biz Inc", null }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "UnitId", "Name", "Notes" },
                values: new object[,]
                {
                    { "236bbc46-221f-4d3e-8ab8-4df84a4704e8", "Piece", null },
                    { "a0867397-4142-46f5-a0fd-f12b4e224b01", "Box", null },
                    { "e2ae3d55-d7b5-4b5b-a263-93f019543c62", "Pack", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "EndTime", "FirstName", "LastName", "Password", "Role", "StartTime", "UserName" },
                values: new object[,]
                {
                    { "1032808d-8b4d-43a4-92e1-0aeb2015e526", new TimeSpan(0, 17, 0, 0, 0), "John", "Doe", "$2a$11$C/yKwAw2sqD2u9zEWmkcVeCQBM0594ZhXzIZFSuvZ78760iubWtT2", 0, new TimeSpan(0, 9, 0, 0, 0), "admin1" },
                    { "a60c93e5-a443-4490-92a1-1f114359c09a", new TimeSpan(0, 18, 0, 0, 0), "Alice", "Johnson", "$2a$11$hfJnaeH/4agUjpjUN9cla.Wtdk5Qy7CHdCgUogQ.Hlgc079wXaK.e", 1, new TimeSpan(0, 10, 0, 0, 0), "alice.johnson" },
                    { "e1343516-a29d-458a-a03a-c9352b1ac80f", new TimeSpan(0, 16, 0, 0, 0), "Jane", "Smith", "$2a$11$ikucYIr2aK.PxfTTQWuw4elOEjrnF79QJUkduJ/JPP2gQcqUc4/K6", 1, new TimeSpan(0, 8, 0, 0, 0), "jane.smith" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CompanyId", "Name", "Notes" },
                values: new object[,]
                {
                    { "5481552c-42c9-47ab-aeab-7e7fd4a9df3d", "f39227bf-be19-43b8-9cfd-6bce63404190", "Furniture", null },
                    { "809b0924-652f-4758-a779-c8476775d639", "d36cc123-8515-4cec-afc2-65ad7a315578", "Clothing", null },
                    { "ea6d20db-6e8f-4394-b384-e3977b309bb9", "34e7fd45-27b4-44e6-b4d7-a8bdf9b979ff", "Electronics", null }
                });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "Id", "BillDate", "ClientId", "Date", "PaidAmount", "TotalAmount", "TotalDiscount", "UserId" },
                values: new object[,]
                {
                    { "1aca300d-0dd6-48d3-bce1-00b2ddacdbfa", new DateTime(2024, 9, 23, 1, 47, 24, 105, DateTimeKind.Local).AddTicks(7760), "5abff7b1-5c8f-4228-a132-2e0487050ca5", new DateTime(2024, 9, 23, 1, 47, 24, 105, DateTimeKind.Local).AddTicks(7826), 1000m, 1000m, 0m, "1032808d-8b4d-43a4-92e1-0aeb2015e526" },
                    { "4fa5da51-71fe-44d9-a900-a57512fea3c1", new DateTime(2024, 9, 23, 1, 47, 24, 105, DateTimeKind.Local).AddTicks(7841), "2fa4aa37-e828-43a0-9654-09195e1fd031", new DateTime(2024, 9, 23, 1, 47, 24, 105, DateTimeKind.Local).AddTicks(7850), 20m, 20m, 0m, "a60c93e5-a443-4490-92a1-1f114359c09a" },
                    { "abb18280-7675-480f-986b-31918fd40773", new DateTime(2024, 9, 23, 1, 47, 24, 105, DateTimeKind.Local).AddTicks(7832), "b37b4bde-c4de-4bf8-bd10-961e185e045f", new DateTime(2024, 9, 23, 1, 47, 24, 105, DateTimeKind.Local).AddTicks(7837), 50m, 50m, 0m, "e1343516-a29d-458a-a03a-c9352b1ac80f" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "BuyingPrice", "CategoryId", "CompanyId", "Name", "Notes", "Quantity", "SellingPrice", "UnitId" },
                values: new object[,]
                {
                    { "02d01a", 10m, "809b0924-652f-4758-a779-c8476775d639", "d36cc123-8515-4cec-afc2-65ad7a315578", "T-Shirt", null, 200m, 20m, "e2ae3d55-d7b5-4b5b-a263-93f019543c62" },
                    { "7e376f", 30m, "5481552c-42c9-47ab-aeab-7e7fd4a9df3d", "f39227bf-be19-43b8-9cfd-6bce63404190", "Chair", null, 100m, 50m, "a0867397-4142-46f5-a0fd-f12b4e224b01" },
                    { "90cbeb", 800m, "ea6d20db-6e8f-4394-b384-e3977b309bb9", "34e7fd45-27b4-44e6-b4d7-a8bdf9b979ff", "Laptop", null, 10m, 1000m, "236bbc46-221f-4d3e-8ab8-4df84a4704e8" }
                });

            migrationBuilder.InsertData(
                table: "InvoiceItems",
                columns: new[] { "Id", "InvoiceId", "Price", "ProductId", "Quantity", "UnitId" },
                values: new object[,]
                {
                    { "05a3dd83-3cad-4a8a-8fb8-4c6c97c7c65a", "abb18280-7675-480f-986b-31918fd40773", 50m, "7e376f", 1m, "a0867397-4142-46f5-a0fd-f12b4e224b01" },
                    { "0b41a6c8-f0c8-4f21-a5c1-7600f7df3b2e", "1aca300d-0dd6-48d3-bce1-00b2ddacdbfa", 1000m, "90cbeb", 1m, "236bbc46-221f-4d3e-8ab8-4df84a4704e8" },
                    { "711a060b-e737-4d8a-870c-cc9db63d4ab3", "4fa5da51-71fe-44d9-a900-a57512fea3c1", 20m, "02d01a", 1m, "e2ae3d55-d7b5-4b5b-a263-93f019543c62" }
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
