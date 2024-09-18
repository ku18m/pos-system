using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PosSystem.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class removeUniqueContrainsOnProductsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CompanyId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UnitId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "InvoiceItems",
                keyColumn: "Id",
                keyValue: "6d72b613-e248-47e6-8273-761870949448");

            migrationBuilder.DeleteData(
                table: "InvoiceItems",
                keyColumn: "Id",
                keyValue: "93bbbf8a-c437-48b3-bfd1-a98274871455");

            migrationBuilder.DeleteData(
                table: "InvoiceItems",
                keyColumn: "Id",
                keyValue: "dfab5abf-665c-43dc-82e8-2c8a44643fb6");

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: "013e9469-9f4a-4b07-9d00-d21d44afc4b3");

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: "5fb504a4-2ee5-4a4c-a01c-bfe997cadb16");

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: "a6be97f4-31ff-4c6a-8740-902351064c4f");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "2c1167");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "cefb5e");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "e19ce4");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "1792c9c9-285e-439a-bc94-97e3d6c64dca");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "5ee2e819-a35e-4356-870a-40f3dab99a96");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "d2c7bc49-90b4-47c8-9ed6-df23c974f284");

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: "7eb580b7-fd3a-49fb-a8e2-364d1e9d8482");

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: "da314426-bcd2-492f-a2b5-39628a6f202d");

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: "eb1a6e87-657b-4c21-905e-e345f4bd2fff");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: "1b3ed427-373b-431e-829b-cb46a619f80e");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: "9d5b276a-5c24-4672-9ed9-34cfe5cca9ea");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: "a099b55b-0330-423a-b2e3-90bc388dfe99");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "baca97ee-f2da-4e63-be4b-54a004200d2a");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "bf3d2e89-6ba6-4794-b6b7-7f9ee3ca6cfe");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "cc655fe4-ba86-48a2-aa62-05d454a24b4d");

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: "34111bfb-255b-4346-9d36-3beb91fa1c27");

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: "6e5f48e3-408c-480a-9833-31475d0a481d");

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: "e174e132-9e37-407b-9e85-8747e90d7f53");

            migrationBuilder.CreateTable(
                name: "SequenceValue",
                columns: table => new
                {
                    CurrentValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientId", "Address", "Name", "Notes", "Number", "Phone" },
                values: new object[,]
                {
                    { "af624889-c6b4-4bb8-9d7a-0f2a97412d78", "789 Oak St", "Jim", null, 3, "1112223333" },
                    { "c9d9115c-6a73-426f-88d1-d8a6e8d53c6b", "123 Main St", "John", null, 1, "1234567890" },
                    { "e47fb905-5507-4ffe-a9c3-b0e88353d3ae", "456 Elm St", "Jane", null, 2, "0987654321" }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "Name", "Notes" },
                values: new object[,]
                {
                    { "47e67d00-09d4-471f-871e-06f6d2c02ace", "Tech Corp", null },
                    { "5b8df501-b523-44fc-ac63-8a1397db0411", "Retail LLC", null },
                    { "a54e1194-2813-4739-8f71-50045064ed6e", "Biz Inc", null }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "UnitId", "Name", "Notes" },
                values: new object[,]
                {
                    { "35c5d8e1-cca2-48d5-b1a4-e08e43f05190", "Box", null },
                    { "5d5b0163-cbe2-4fd5-96f0-10ca530110b4", "Piece", null },
                    { "c3da37d8-206e-4674-8b46-29726ce2203a", "Pack", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "EndTime", "FirstName", "LastName", "Password", "Role", "StartTime", "UserName" },
                values: new object[,]
                {
                    { "3f58592f-8bfb-41a0-a778-2eb1ba67513e", new TimeSpan(0, 16, 0, 0, 0), "Jane", "Smith", "$2a$11$soiDwcYdMoES/uv4LJRIJuTeT5AiWwMVqHiyQwMP.k8JMgFsy.P/S", 1, new TimeSpan(0, 8, 0, 0, 0), "jane.smith" },
                    { "6c60cd73-7ff9-4ac6-9782-8820729aa023", new TimeSpan(0, 17, 0, 0, 0), "John", "Doe", "$2a$11$I.lASZn25x/I3MFw1s6sSO1/XWA1W/dRY9Yo74PxwEC6ees/MiZHu", 0, new TimeSpan(0, 9, 0, 0, 0), "admin1" },
                    { "f9a25338-4959-40d0-8eb5-0ba163fdea30", new TimeSpan(0, 18, 0, 0, 0), "Alice", "Johnson", "$2a$11$LlMgCMi.FjVcwnK1HAhtX.NDY/O98AyosDOBch55ooHfvxxwnK4NS", 1, new TimeSpan(0, 10, 0, 0, 0), "alice.johnson" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CompanyId", "Name", "Notes" },
                values: new object[,]
                {
                    { "76c06eba-94de-4808-a4cd-33023bbf752d", "5b8df501-b523-44fc-ac63-8a1397db0411", "Clothing", null },
                    { "b84383cc-b15c-41bb-8317-d106b7410247", "a54e1194-2813-4739-8f71-50045064ed6e", "Furniture", null },
                    { "c14bc497-f2ef-4f7e-a6ee-0fd39c2559d0", "47e67d00-09d4-471f-871e-06f6d2c02ace", "Electronics", null }
                });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "Id", "BillDate", "BillNumber", "ClientId", "Date", "PaidAmount", "TotalAmount", "TotalDiscount", "UserId" },
                values: new object[,]
                {
                    { "3751de2c-cccf-4d22-8388-4ff2f8b00054", new DateTime(2024, 9, 18, 5, 6, 28, 464, DateTimeKind.Local).AddTicks(4985), 3L, "af624889-c6b4-4bb8-9d7a-0f2a97412d78", new DateTime(2024, 9, 18, 5, 6, 28, 464, DateTimeKind.Local).AddTicks(4995), 20m, 20m, 0m, "f9a25338-4959-40d0-8eb5-0ba163fdea30" },
                    { "7db68675-bcd2-49c7-aa72-396a7ad626d1", new DateTime(2024, 9, 18, 5, 6, 28, 464, DateTimeKind.Local).AddTicks(4972), 2L, "e47fb905-5507-4ffe-a9c3-b0e88353d3ae", new DateTime(2024, 9, 18, 5, 6, 28, 464, DateTimeKind.Local).AddTicks(4978), 50m, 50m, 0m, "3f58592f-8bfb-41a0-a778-2eb1ba67513e" },
                    { "a51d7b19-6ee1-4f4c-be14-29aa887776eb", new DateTime(2024, 9, 18, 5, 6, 28, 464, DateTimeKind.Local).AddTicks(4871), 1L, "c9d9115c-6a73-426f-88d1-d8a6e8d53c6b", new DateTime(2024, 9, 18, 5, 6, 28, 464, DateTimeKind.Local).AddTicks(4953), 1000m, 1000m, 0m, "6c60cd73-7ff9-4ac6-9782-8820729aa023" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "BuyingPrice", "CategoryId", "CompanyId", "Name", "Notes", "Quantity", "SellingPrice", "UnitId" },
                values: new object[,]
                {
                    { "16d751", 30m, "b84383cc-b15c-41bb-8317-d106b7410247", "a54e1194-2813-4739-8f71-50045064ed6e", "Chair", null, 100, 50m, "35c5d8e1-cca2-48d5-b1a4-e08e43f05190" },
                    { "c70b91", 800m, "c14bc497-f2ef-4f7e-a6ee-0fd39c2559d0", "47e67d00-09d4-471f-871e-06f6d2c02ace", "Laptop", null, 10, 1000m, "5d5b0163-cbe2-4fd5-96f0-10ca530110b4" },
                    { "cf7f7e", 10m, "76c06eba-94de-4808-a4cd-33023bbf752d", "5b8df501-b523-44fc-ac63-8a1397db0411", "T-Shirt", null, 200, 20m, "c3da37d8-206e-4674-8b46-29726ce2203a" }
                });

            migrationBuilder.InsertData(
                table: "InvoiceItems",
                columns: new[] { "Id", "Discount", "InvoiceId", "Price", "ProductId", "Quantity", "UnitId" },
                values: new object[,]
                {
                    { "5baed914-c1cc-4a84-8ee1-999929cf40c2", 0m, "3751de2c-cccf-4d22-8388-4ff2f8b00054", 20m, "cf7f7e", 1m, "c3da37d8-206e-4674-8b46-29726ce2203a" },
                    { "7e42d6e3-701d-40ae-97ad-d34ebb4f53a5", 0m, "7db68675-bcd2-49c7-aa72-396a7ad626d1", 50m, "16d751", 1m, "35c5d8e1-cca2-48d5-b1a4-e08e43f05190" },
                    { "91749636-98e6-427a-a994-60c6d760d8af", 0m, "a51d7b19-6ee1-4f4c-be14-29aa887776eb", 1000m, "c70b91", 1m, "5d5b0163-cbe2-4fd5-96f0-10ca530110b4" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CompanyId",
                table: "Products",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitId",
                table: "Products",
                column: "UnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SequenceValue");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CompanyId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UnitId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "InvoiceItems",
                keyColumn: "Id",
                keyValue: "5baed914-c1cc-4a84-8ee1-999929cf40c2");

            migrationBuilder.DeleteData(
                table: "InvoiceItems",
                keyColumn: "Id",
                keyValue: "7e42d6e3-701d-40ae-97ad-d34ebb4f53a5");

            migrationBuilder.DeleteData(
                table: "InvoiceItems",
                keyColumn: "Id",
                keyValue: "91749636-98e6-427a-a994-60c6d760d8af");

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: "3751de2c-cccf-4d22-8388-4ff2f8b00054");

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: "7db68675-bcd2-49c7-aa72-396a7ad626d1");

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: "a51d7b19-6ee1-4f4c-be14-29aa887776eb");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "16d751");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "c70b91");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "cf7f7e");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "76c06eba-94de-4808-a4cd-33023bbf752d");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "b84383cc-b15c-41bb-8317-d106b7410247");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "c14bc497-f2ef-4f7e-a6ee-0fd39c2559d0");

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: "af624889-c6b4-4bb8-9d7a-0f2a97412d78");

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: "c9d9115c-6a73-426f-88d1-d8a6e8d53c6b");

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: "e47fb905-5507-4ffe-a9c3-b0e88353d3ae");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: "35c5d8e1-cca2-48d5-b1a4-e08e43f05190");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: "5d5b0163-cbe2-4fd5-96f0-10ca530110b4");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "UnitId",
                keyValue: "c3da37d8-206e-4674-8b46-29726ce2203a");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "3f58592f-8bfb-41a0-a778-2eb1ba67513e");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "6c60cd73-7ff9-4ac6-9782-8820729aa023");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "f9a25338-4959-40d0-8eb5-0ba163fdea30");

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: "47e67d00-09d4-471f-871e-06f6d2c02ace");

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: "5b8df501-b523-44fc-ac63-8a1397db0411");

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: "a54e1194-2813-4739-8f71-50045064ed6e");

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
                name: "IX_Products_UnitId",
                table: "Products",
                column: "UnitId",
                unique: true);
        }
    }
}
