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
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
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
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.ClientNumber"),
                    Phone = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_Clients_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
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
                    UnitId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
                        name: "FK_Products_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK_Products_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "UnitId");
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
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false)
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
                        name: "FK_Invoices_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
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
                columns: new[] { "ClientId", "Address", "EmployeeId", "FirstName", "LastName", "Notes", "Number", "Phone" },
                values: new object[,]
                {
                    { "3d1cae27-407d-4901-ac90-35e972ec813a", "456 Elm St", null, "Jane", "Doe", null, 2, "0987654321" },
                    { "521cfe59-b916-4f4d-a2a3-0f9d023cbbb2", "123 Main St", null, "John", "Doe", null, 1, "1234567890" },
                    { "c918b848-0e0d-4d94-990f-fd1a7bc9e445", "789 Oak St", null, "Jim", "Beam", null, 3, "1112223333" }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "Name", "Notes" },
                values: new object[,]
                {
                    { "84057f93-c9d6-46cf-b018-e3ffd5cda383", "Tech Corp", null },
                    { "a7fd0fb6-871d-4452-9b5a-7a376d8fa3c7", "Biz Inc", null },
                    { "c1c55d49-79bd-4ca4-9b6a-afc74e371bfd", "Retail LLC", null }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Address", "DateOfBirth", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { "0a8f0244-4c3a-4336-9bd9-ec9180807951", null, new DateTime(1990, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bob", "Johnson", null },
                    { "3ff029bf-a4a2-4510-91c6-5301fe4ec389", null, new DateTime(1995, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Charlie", "Brown", null },
                    { "895fcfbd-0257-4a6b-ad18-8686e19b7fae", null, new DateTime(1985, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alice", "Smith", null }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "UnitId", "Name", "Notes" },
                values: new object[,]
                {
                    { "017cf13d-ce50-429e-b337-e115ba1d92cf", "Box", null },
                    { "27cb9365-ade2-471e-be53-a13e67c6ed7e", "Piece", null },
                    { "d396c365-d491-4d48-adc4-e3a13caf8440", "Pack", null }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CompanyId", "Name", "Notes" },
                values: new object[,]
                {
                    { "17caa7db-220c-478c-b049-be42900c3c95", "84057f93-c9d6-46cf-b018-e3ffd5cda383", "Electronics", null },
                    { "2ea866bf-d98c-4da1-a99d-f67c0df95681", "a7fd0fb6-871d-4452-9b5a-7a376d8fa3c7", "Furniture", null },
                    { "634430fe-2f01-42d5-aaf1-eda47c9ba543", "c1c55d49-79bd-4ca4-9b6a-afc74e371bfd", "Clothing", null }
                });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "Id", "BillDate", "BillNumber", "ClientId", "Date", "EmployeeId", "EndTime", "PaidAmount", "StartTime", "TotalAmount", "TotalDiscount" },
                values: new object[,]
                {
                    { "75cd73c7-f86a-48a5-b6de-0991f80b8649", new DateTime(2024, 9, 11, 0, 29, 19, 957, DateTimeKind.Local).AddTicks(9725), 2L, "3d1cae27-407d-4901-ac90-35e972ec813a", new DateTime(2024, 9, 11, 0, 29, 19, 957, DateTimeKind.Local).AddTicks(9729), "0a8f0244-4c3a-4336-9bd9-ec9180807951", new TimeSpan(0, 18, 0, 0, 0), 50m, new TimeSpan(0, 10, 0, 0, 0), 50m, 0m },
                    { "93f4fff1-2a07-482b-97ab-fcef3be93565", new DateTime(2024, 9, 11, 0, 29, 19, 957, DateTimeKind.Local).AddTicks(9672), 1L, "521cfe59-b916-4f4d-a2a3-0f9d023cbbb2", new DateTime(2024, 9, 11, 0, 29, 19, 957, DateTimeKind.Local).AddTicks(9718), "895fcfbd-0257-4a6b-ad18-8686e19b7fae", new TimeSpan(0, 17, 0, 0, 0), 1000m, new TimeSpan(0, 9, 0, 0, 0), 1000m, 0m },
                    { "d59456cc-77ab-4d88-8069-841a86845736", new DateTime(2024, 9, 11, 0, 29, 19, 957, DateTimeKind.Local).AddTicks(9733), 3L, "c918b848-0e0d-4d94-990f-fd1a7bc9e445", new DateTime(2024, 9, 11, 0, 29, 19, 957, DateTimeKind.Local).AddTicks(9737), "3ff029bf-a4a2-4510-91c6-5301fe4ec389", new TimeSpan(0, 19, 0, 0, 0), 20m, new TimeSpan(0, 11, 0, 0, 0), 20m, 0m }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "BuyingPrice", "CategoryId", "CompanyId", "EmployeeId", "Name", "Notes", "Quantity", "SellingPrice", "UnitId" },
                values: new object[,]
                {
                    { "79d247", 800m, "17caa7db-220c-478c-b049-be42900c3c95", "84057f93-c9d6-46cf-b018-e3ffd5cda383", null, "Laptop", null, 10, 1000m, "27cb9365-ade2-471e-be53-a13e67c6ed7e" },
                    { "8404e5", 10m, "634430fe-2f01-42d5-aaf1-eda47c9ba543", "c1c55d49-79bd-4ca4-9b6a-afc74e371bfd", null, "T-Shirt", null, 200, 20m, "d396c365-d491-4d48-adc4-e3a13caf8440" },
                    { "af9d5b", 30m, "2ea866bf-d98c-4da1-a99d-f67c0df95681", "a7fd0fb6-871d-4452-9b5a-7a376d8fa3c7", null, "Chair", null, 100, 50m, "017cf13d-ce50-429e-b337-e115ba1d92cf" }
                });

            migrationBuilder.InsertData(
                table: "InvoiceItems",
                columns: new[] { "Id", "Discount", "InvoiceId", "Price", "ProductId", "Quantity", "UnitId" },
                values: new object[,]
                {
                    { "400b611d-39de-45b8-b34d-20fd4c1a462a", 0m, "75cd73c7-f86a-48a5-b6de-0991f80b8649", 50m, "af9d5b", 1m, "017cf13d-ce50-429e-b337-e115ba1d92cf" },
                    { "44cd904f-82e9-420f-987a-8897c39ebc57", 0m, "93f4fff1-2a07-482b-97ab-fcef3be93565", 1000m, "79d247", 1m, "27cb9365-ade2-471e-be53-a13e67c6ed7e" },
                    { "b5f6786a-c57f-40bb-8c3f-fb0c0a2c65ab", 0m, "d59456cc-77ab-4d88-8069-841a86845736", 20m, "8404e5", 1m, "d396c365-d491-4d48-adc4-e3a13caf8440" }
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
                name: "IX_Clients_EmployeeId",
                table: "Clients",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_FirstName",
                table: "Clients",
                column: "FirstName");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_LastName",
                table: "Clients",
                column: "LastName");

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
                name: "IX_Employees_FirstName",
                table: "Employees",
                column: "FirstName");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_LastName",
                table: "Employees",
                column: "LastName");

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
                name: "IX_Invoices_EmployeeId",
                table: "Invoices",
                column: "EmployeeId");

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
                name: "IX_Products_EmployeeId",
                table: "Products",
                column: "EmployeeId");

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
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Employees");

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
