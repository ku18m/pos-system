﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PosSystem.Infrastracture.Persistence.Data;

#nullable disable

namespace PosSystem.Infrastracture.Migrations
{
    [DbContext(typeof(PosDbContext))]
    partial class PosDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence<int>("BillNumber", "dbo")
                .StartsAt(1000000L);

            modelBuilder.HasSequence<int>("ClientNumber", "dbo")
                .StartsAt(1000L);

            modelBuilder.Entity("PosSystem.Core.Entities.Category", b =>
                {
                    b.Property<string>("CategoryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompanyId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = "ea6d20db-6e8f-4394-b384-e3977b309bb9",
                            CompanyId = "34e7fd45-27b4-44e6-b4d7-a8bdf9b979ff",
                            Name = "Electronics"
                        },
                        new
                        {
                            CategoryId = "5481552c-42c9-47ab-aeab-7e7fd4a9df3d",
                            CompanyId = "f39227bf-be19-43b8-9cfd-6bce63404190",
                            Name = "Furniture"
                        },
                        new
                        {
                            CategoryId = "809b0924-652f-4758-a779-c8476775d639",
                            CompanyId = "d36cc123-8515-4cec-afc2-65ad7a315578",
                            Name = "Clothing"
                        });
                });

            modelBuilder.Entity("PosSystem.Core.Entities.Client", b =>
                {
                    b.Property<string>("ClientId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR dbo.ClientNumber");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ClientId");

                    b.HasIndex("Address");

                    b.HasIndex("Name");

                    b.HasIndex("Number")
                        .IsUnique();

                    b.HasIndex("Phone");

                    b.ToTable("Clients");

                    b.HasData(
                        new
                        {
                            ClientId = "5abff7b1-5c8f-4228-a132-2e0487050ca5",
                            Address = "123 Main St",
                            Name = "John",
                            Number = 0,
                            Phone = "1234567890"
                        },
                        new
                        {
                            ClientId = "b37b4bde-c4de-4bf8-bd10-961e185e045f",
                            Address = "456 Elm St",
                            Name = "Jane",
                            Number = 0,
                            Phone = "0987654321"
                        },
                        new
                        {
                            ClientId = "2fa4aa37-e828-43a0-9654-09195e1fd031",
                            Address = "789 Oak St",
                            Name = "Jim",
                            Number = 0,
                            Phone = "1112223333"
                        });
                });

            modelBuilder.Entity("PosSystem.Core.Entities.Company", b =>
                {
                    b.Property<string>("CompanyId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompanyId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            CompanyId = "34e7fd45-27b4-44e6-b4d7-a8bdf9b979ff",
                            Name = "Tech Corp"
                        },
                        new
                        {
                            CompanyId = "f39227bf-be19-43b8-9cfd-6bce63404190",
                            Name = "Biz Inc"
                        },
                        new
                        {
                            CompanyId = "d36cc123-8515-4cec-afc2-65ad7a315578",
                            Name = "Retail LLC"
                        });
                });

            modelBuilder.Entity("PosSystem.Core.Entities.Invoice", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("BillDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("BillNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR dbo.BillNumber");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("DueAmount")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("decimal(18, 2)")
                        .HasComputedColumnSql("[TotalAmount] - [TotalDiscount] - [PaidAmount]");

                    b.Property<decimal>("FinalAmount")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("decimal(18, 2)")
                        .HasComputedColumnSql("[TotalAmount] - [TotalDiscount]");

                    b.Property<decimal>("PaidAmount")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal>("TotalDiscount")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("BillDate");

                    b.HasIndex("BillNumber")
                        .IsUnique();

                    b.HasIndex("ClientId");

                    b.HasIndex("Date");

                    b.HasIndex("UserId");

                    b.ToTable("Invoices");

                    b.HasData(
                        new
                        {
                            Id = "1aca300d-0dd6-48d3-bce1-00b2ddacdbfa",
                            BillDate = new DateTime(2024, 9, 23, 1, 47, 24, 105, DateTimeKind.Local).AddTicks(7760),
                            BillNumber = 0,
                            ClientId = "5abff7b1-5c8f-4228-a132-2e0487050ca5",
                            Date = new DateTime(2024, 9, 23, 1, 47, 24, 105, DateTimeKind.Local).AddTicks(7826),
                            DueAmount = 0m,
                            FinalAmount = 1000m,
                            PaidAmount = 1000m,
                            TotalAmount = 1000m,
                            TotalDiscount = 0m,
                            UserId = "1032808d-8b4d-43a4-92e1-0aeb2015e526"
                        },
                        new
                        {
                            Id = "abb18280-7675-480f-986b-31918fd40773",
                            BillDate = new DateTime(2024, 9, 23, 1, 47, 24, 105, DateTimeKind.Local).AddTicks(7832),
                            BillNumber = 0,
                            ClientId = "b37b4bde-c4de-4bf8-bd10-961e185e045f",
                            Date = new DateTime(2024, 9, 23, 1, 47, 24, 105, DateTimeKind.Local).AddTicks(7837),
                            DueAmount = 0m,
                            FinalAmount = 50m,
                            PaidAmount = 50m,
                            TotalAmount = 50m,
                            TotalDiscount = 0m,
                            UserId = "e1343516-a29d-458a-a03a-c9352b1ac80f"
                        },
                        new
                        {
                            Id = "4fa5da51-71fe-44d9-a900-a57512fea3c1",
                            BillDate = new DateTime(2024, 9, 23, 1, 47, 24, 105, DateTimeKind.Local).AddTicks(7841),
                            BillNumber = 0,
                            ClientId = "2fa4aa37-e828-43a0-9654-09195e1fd031",
                            Date = new DateTime(2024, 9, 23, 1, 47, 24, 105, DateTimeKind.Local).AddTicks(7850),
                            DueAmount = 0m,
                            FinalAmount = 20m,
                            PaidAmount = 20m,
                            TotalAmount = 20m,
                            TotalDiscount = 0m,
                            UserId = "a60c93e5-a443-4490-92a1-1f114359c09a"
                        });
                });

            modelBuilder.Entity("PosSystem.Core.Entities.InvoiceItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("InvoiceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal>("TotalAmount")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("decimal(18, 2)")
                        .HasComputedColumnSql("[Quantity] * [Price]");

                    b.Property<string>("UnitId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UnitId");

                    b.ToTable("InvoiceItems");

                    b.HasData(
                        new
                        {
                            Id = "0b41a6c8-f0c8-4f21-a5c1-7600f7df3b2e",
                            InvoiceId = "1aca300d-0dd6-48d3-bce1-00b2ddacdbfa",
                            Price = 1000m,
                            ProductId = "90cbeb",
                            Quantity = 1m,
                            TotalAmount = 1000m,
                            UnitId = "236bbc46-221f-4d3e-8ab8-4df84a4704e8"
                        },
                        new
                        {
                            Id = "05a3dd83-3cad-4a8a-8fb8-4c6c97c7c65a",
                            InvoiceId = "abb18280-7675-480f-986b-31918fd40773",
                            Price = 50m,
                            ProductId = "7e376f",
                            Quantity = 1m,
                            TotalAmount = 50m,
                            UnitId = "a0867397-4142-46f5-a0fd-f12b4e224b01"
                        },
                        new
                        {
                            Id = "711a060b-e737-4d8a-870c-cc9db63d4ab3",
                            InvoiceId = "4fa5da51-71fe-44d9-a900-a57512fea3c1",
                            Price = 20m,
                            ProductId = "02d01a",
                            Quantity = 1m,
                            TotalAmount = 20m,
                            UnitId = "e2ae3d55-d7b5-4b5b-a263-93f019543c62"
                        });
                });

            modelBuilder.Entity("PosSystem.Core.Entities.Product", b =>
                {
                    b.Property<string>("ProductId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("BuyingPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("CategoryId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompanyId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal>("SellingPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("UnitId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("UnitId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = "90cbeb",
                            BuyingPrice = 800m,
                            CategoryId = "ea6d20db-6e8f-4394-b384-e3977b309bb9",
                            CompanyId = "34e7fd45-27b4-44e6-b4d7-a8bdf9b979ff",
                            Name = "Laptop",
                            Quantity = 10m,
                            SellingPrice = 1000m,
                            UnitId = "236bbc46-221f-4d3e-8ab8-4df84a4704e8"
                        },
                        new
                        {
                            ProductId = "7e376f",
                            BuyingPrice = 30m,
                            CategoryId = "5481552c-42c9-47ab-aeab-7e7fd4a9df3d",
                            CompanyId = "f39227bf-be19-43b8-9cfd-6bce63404190",
                            Name = "Chair",
                            Quantity = 100m,
                            SellingPrice = 50m,
                            UnitId = "a0867397-4142-46f5-a0fd-f12b4e224b01"
                        },
                        new
                        {
                            ProductId = "02d01a",
                            BuyingPrice = 10m,
                            CategoryId = "809b0924-652f-4758-a779-c8476775d639",
                            CompanyId = "d36cc123-8515-4cec-afc2-65ad7a315578",
                            Name = "T-Shirt",
                            Quantity = 200m,
                            SellingPrice = 20m,
                            UnitId = "e2ae3d55-d7b5-4b5b-a263-93f019543c62"
                        });
                });

            modelBuilder.Entity("PosSystem.Core.Entities.Unit", b =>
                {
                    b.Property<string>("UnitId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UnitId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Units");

                    b.HasData(
                        new
                        {
                            UnitId = "236bbc46-221f-4d3e-8ab8-4df84a4704e8",
                            Name = "Piece"
                        },
                        new
                        {
                            UnitId = "a0867397-4142-46f5-a0fd-f12b4e224b01",
                            Name = "Box"
                        },
                        new
                        {
                            UnitId = "e2ae3d55-d7b5-4b5b-a263-93f019543c62",
                            Name = "Pack"
                        });
                });

            modelBuilder.Entity("PosSystem.Core.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("FirstName");

                    b.HasIndex("LastName");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = "1032808d-8b4d-43a4-92e1-0aeb2015e526",
                            EndTime = new TimeSpan(0, 17, 0, 0, 0),
                            FirstName = "John",
                            LastName = "Doe",
                            Password = "$2a$11$C/yKwAw2sqD2u9zEWmkcVeCQBM0594ZhXzIZFSuvZ78760iubWtT2",
                            Role = 0,
                            StartTime = new TimeSpan(0, 9, 0, 0, 0),
                            UserName = "admin1"
                        },
                        new
                        {
                            Id = "e1343516-a29d-458a-a03a-c9352b1ac80f",
                            EndTime = new TimeSpan(0, 16, 0, 0, 0),
                            FirstName = "Jane",
                            LastName = "Smith",
                            Password = "$2a$11$ikucYIr2aK.PxfTTQWuw4elOEjrnF79QJUkduJ/JPP2gQcqUc4/K6",
                            Role = 1,
                            StartTime = new TimeSpan(0, 8, 0, 0, 0),
                            UserName = "jane.smith"
                        },
                        new
                        {
                            Id = "a60c93e5-a443-4490-92a1-1f114359c09a",
                            EndTime = new TimeSpan(0, 18, 0, 0, 0),
                            FirstName = "Alice",
                            LastName = "Johnson",
                            Password = "$2a$11$hfJnaeH/4agUjpjUN9cla.Wtdk5Qy7CHdCgUogQ.Hlgc079wXaK.e",
                            Role = 1,
                            StartTime = new TimeSpan(0, 10, 0, 0, 0),
                            UserName = "alice.johnson"
                        });
                });

            modelBuilder.Entity("PosSystem.Core.Entities.Category", b =>
                {
                    b.HasOne("PosSystem.Core.Entities.Company", "Company")
                        .WithMany("Categories")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("PosSystem.Core.Entities.Invoice", b =>
                {
                    b.HasOne("PosSystem.Core.Entities.Client", "Client")
                        .WithMany("Invoices")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PosSystem.Core.Entities.User", "User")
                        .WithMany("Invoices")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PosSystem.Core.Entities.InvoiceItem", b =>
                {
                    b.HasOne("PosSystem.Core.Entities.Invoice", "Invoice")
                        .WithMany("Items")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PosSystem.Core.Entities.Product", "Product")
                        .WithMany("InvoiceItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PosSystem.Core.Entities.Unit", "Unit")
                        .WithMany("InvoiceItems")
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Invoice");

                    b.Navigation("Product");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("PosSystem.Core.Entities.Product", b =>
                {
                    b.HasOne("PosSystem.Core.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PosSystem.Core.Entities.Company", "Company")
                        .WithMany("Products")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PosSystem.Core.Entities.Unit", "Unit")
                        .WithMany("Products")
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Company");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("PosSystem.Core.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("PosSystem.Core.Entities.Client", b =>
                {
                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("PosSystem.Core.Entities.Company", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("PosSystem.Core.Entities.Invoice", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("PosSystem.Core.Entities.Product", b =>
                {
                    b.Navigation("InvoiceItems");
                });

            modelBuilder.Entity("PosSystem.Core.Entities.Unit", b =>
                {
                    b.Navigation("InvoiceItems");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("PosSystem.Core.Entities.User", b =>
                {
                    b.Navigation("Invoices");
                });
#pragma warning restore 612, 618
        }
    }
}
