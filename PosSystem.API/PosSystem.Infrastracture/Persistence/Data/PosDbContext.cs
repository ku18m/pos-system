using Microsoft.EntityFrameworkCore;
using PosSystem.Core.Entities;

namespace PosSystem.Infrastracture.Persistence.Data
{
    public class PosDbContext : DbContext
    {

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<User> Users { get; set; }


        public PosDbContext(DbContextOptions<PosDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Main_Sequences
            // Define The BillNumber sequence for the Invoice Entity
            modelBuilder.HasSequence<long>("BillNumber", schema: "dbo")
                .StartsAt(1000000)
                .IncrementsBy(1);

            // Define The ClientNumber sequence for the Client Entity
            modelBuilder.HasSequence<int>("ClientNumber", schema: "dbo")
                .StartsAt(1000)
                .IncrementsBy(1);
            #endregion

            #region Entities_Configurations
            // Configure the Invoice Entity
            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.BillNumber)
                    .HasDefaultValueSql("NEXT VALUE FOR dbo.BillNumber");

                entity.HasMany(i => i.Items)
                    .WithOne(ii => ii.Invoice)
                    .HasForeignKey(ii => ii.InvoiceId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(i => i.Client)
                    .WithMany(c => c.Invoices)
                    .HasForeignKey(i => i.ClientId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(i => i.Employee)
                    .WithMany(e => e.Invoices)
                    .HasForeignKey(i => i.EmployeeId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.Property(e => e.TotalAmount)
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalDiscount)
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FinalAmount)
                    .HasColumnType("decimal(18, 2)")
                    .HasComputedColumnSql("[TotalAmount] - [TotalDiscount]");

                entity.Property(e => e.PaidAmount)
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DueAmount)
                    .HasColumnType("decimal(18, 2)")
                    .HasComputedColumnSql("[TotalAmount] - [TotalDiscount] - [PaidAmount]");
            });

            // Configure the Client Entity
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.ClientId);

                entity.Property(e => e.Number)
                    .HasDefaultValueSql("NEXT VALUE FOR dbo.ClientNumber");

                entity.HasMany(c => c.Invoices)
                    .WithOne(i => i.Client)
                    .HasForeignKey(i => i.ClientId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // Configure the Product Entity
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(p => p.Company)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CompanyId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(p => p.Unit)
                    .WithMany(u => u.Products)
                    .HasForeignKey(p => p.UnitId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(p => p.InvoiceItems)
                    .WithOne(ii => ii.Product)
                    .HasForeignKey(ii => ii.ProductId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.Property(e => e.SellingPrice)
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.BuyingPrice)
                    .HasColumnType("decimal(18, 2)");
            });

            // Configure the InvoiceItem Entity
            modelBuilder.Entity<InvoiceItem>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(ii => ii.Invoice)
                    .WithMany(i => i.Items)
                    .HasForeignKey(ii => ii.InvoiceId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(ii => ii.Product)
                    .WithMany(p => p.InvoiceItems)
                    .HasForeignKey(ii => ii.ProductId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.Property(e => e.Quantity)
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalAmount)
                    .HasColumnType("decimal(18, 2)")
                    .HasComputedColumnSql("[Quantity] * [Price]");

                entity.Property(e => e.Discount)
                    .HasColumnType("decimal(18, 2)");
                
                entity.Property(e => e.FinalAmount)
                    .HasColumnType("decimal(18, 2)")
                    .HasComputedColumnSql("([Quantity] * [Price]) - [Discount]");
            });

            // Configure the Category Entity
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.HasMany(c => c.Products)
                    .WithOne(p => p.Category)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // Configure the Company Entity
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.CompanyId);

                entity.HasMany(c => c.Products)
                    .WithOne(p => p.Company)
                    .HasForeignKey(p => p.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(c => c.Categories)
                    .WithOne(c => c.Company)
                    .HasForeignKey(c => c.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure the Unit Entity
            modelBuilder.Entity<Unit>(entity =>
            {
                entity.HasKey(e => e.UnitId);

                entity.HasMany(u => u.Products)
                    .WithOne(p => p.Unit)
                    .HasForeignKey(p => p.UnitId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // Configure the User Entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            // Configure the Employee Entity
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.HasMany(e => e.Invoices)
                    .WithOne(i => i.Employee)
                    .HasForeignKey(i => i.EmployeeId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            #endregion

            #region Indexes
            // Set Invoice Indexes
            modelBuilder.Entity<Invoice>()
                .HasIndex(i => i.BillNumber)
                .IsUnique();

            modelBuilder.Entity<Invoice>()
                .HasIndex(i => i.BillDate);

            modelBuilder.Entity<Invoice>()
                .HasIndex(i => i.ClientId);

            modelBuilder.Entity<Invoice>()
                .HasIndex(i => i.EmployeeId);

            modelBuilder.Entity<Invoice>()
                .HasIndex(i => i.Date);


            // Set InvoiceItem Indexes
            modelBuilder.Entity<InvoiceItem>()
                .HasIndex(ii => ii.InvoiceId);

            modelBuilder.Entity<InvoiceItem>()
                .HasIndex(ii => ii.ProductId);


            // Set Client Indexes
            modelBuilder.Entity<Client>()
                .HasIndex(c => c.Number)
                .IsUnique();

            modelBuilder.Entity<Client>()
                .HasIndex(c => c.FirstName);

            modelBuilder.Entity<Client>()
                .HasIndex(c => c.LastName);

            modelBuilder.Entity<Client>()
                .HasIndex(c => c.Phone);

            modelBuilder.Entity<Client>()
                .HasIndex(c => c.Address);


            // Set Product Indexes
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.CompanyId)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.CategoryId)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.UnitId)
                .IsUnique();


            // Set Category Indexes
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();


            // Set Company Indexes
            modelBuilder.Entity<Company>()
                .HasIndex(c => c.Name)
                .IsUnique();


            // Set Unit Indexes
            modelBuilder.Entity<Unit>()
                .HasIndex(u => u.Name)
                .IsUnique();


            // Set User Indexes
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Set Employee Indexes
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.FirstName);

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.LastName);

            #endregion

            #region Seed_Data
            var clients = new[]
            {
            new Client { FirstName = "John", LastName = "Doe", Number = 1, Phone = "1234567890", Address = "123 Main St" },
            new Client { FirstName = "Jane", LastName = "Doe", Number = 2, Phone = "0987654321", Address = "456 Elm St" },
            new Client { FirstName = "Jim", LastName = "Beam", Number = 3, Phone = "1112223333", Address = "789 Oak St" }
            };

            var employees = new[]
            {
            new Employee { FirstName = "Alice", LastName = "Smith", DateOfBirth = new DateTime(1985, 1, 1) },
            new Employee { FirstName = "Bob", LastName = "Johnson", DateOfBirth = new DateTime(1990, 2, 2) },
            new Employee { FirstName = "Charlie", LastName = "Brown", DateOfBirth = new DateTime(1995, 3, 3) }
            };

            var companies = new[]
            {
            new Company { Name = "Tech Corp" },
            new Company { Name = "Biz Inc" },
            new Company { Name = "Retail LLC" }
            };

            var categories = new[]
            {
            new Category { Name = "Electronics", CompanyId = companies[0].CompanyId },
            new Category { Name = "Furniture", CompanyId = companies[1].CompanyId },
            new Category { Name = "Clothing", CompanyId = companies[2].CompanyId }
            };

            var units = new[]
            {
            new Unit { Name = "Piece" },
            new Unit { Name = "Box" },
            new Unit { Name = "Pack" }
            };

            var products = new[]
            {
            new Product { Name = "Laptop", SellingPrice = 1000, BuyingPrice = 800, Quantity = 10, CategoryId = categories[0].CategoryId, CompanyId = companies[0].CompanyId, UnitId = units[0].UnitId },
            new Product { Name = "Chair", SellingPrice = 50, BuyingPrice = 30, Quantity = 100, CategoryId = categories[1].CategoryId, CompanyId = companies[1].CompanyId, UnitId = units[1].UnitId },
            new Product { Name = "T-Shirt", SellingPrice = 20, BuyingPrice = 10, Quantity = 200, CategoryId = categories[2].CategoryId, CompanyId = companies[2].CompanyId, UnitId = units[2].UnitId }
            };

            var invoices = new[]
            {
            new Invoice { BillDate = DateTime.Now, BillNumber = 1, ClientId = clients[0].ClientId, EmployeeId = employees[0].EmployeeId, TotalAmount = 1000, TotalDiscount = 0, FinalAmount = 1000, PaidAmount = 1000, DueAmount = 0, Date = DateTime.Now, StartTime = TimeSpan.FromHours(9), EndTime = TimeSpan.FromHours(17) },
            new Invoice { BillDate = DateTime.Now, BillNumber = 2, ClientId = clients[1].ClientId, EmployeeId = employees[1].EmployeeId, TotalAmount = 50, TotalDiscount = 0, FinalAmount = 50, PaidAmount = 50, DueAmount = 0, Date = DateTime.Now, StartTime = TimeSpan.FromHours(10), EndTime = TimeSpan.FromHours(18) },
            new Invoice { BillDate = DateTime.Now, BillNumber = 3, ClientId = clients[2].ClientId, EmployeeId = employees[2].EmployeeId, TotalAmount = 20, TotalDiscount = 0, FinalAmount = 20, PaidAmount = 20, DueAmount = 0, Date = DateTime.Now, StartTime = TimeSpan.FromHours(11), EndTime = TimeSpan.FromHours(19) }
            };

            var invoiceItems = new[]
            {
            new InvoiceItem { InvoiceId = invoices[0].Id, ProductId = products[0].ProductId, UnitId = units[0].UnitId, Quantity = 1, Price = 1000, TotalAmount = 1000, Discount = 0, FinalAmount = 1000 },
            new InvoiceItem { InvoiceId = invoices[1].Id, ProductId = products[1].ProductId, UnitId = units[1].UnitId, Quantity = 1, Price = 50, TotalAmount = 50, Discount = 0, FinalAmount = 50 },
            new InvoiceItem { InvoiceId = invoices[2].Id, ProductId = products[2].ProductId, UnitId = units[2].UnitId, Quantity = 1, Price = 20, TotalAmount = 20, Discount = 0, FinalAmount = 20 }
            };

            modelBuilder.Entity<Client>().HasData(clients);
            modelBuilder.Entity<Employee>().HasData(employees);
            modelBuilder.Entity<Company>().HasData(companies);
            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<Unit>().HasData(units);
            modelBuilder.Entity<Product>().HasData(products);
            modelBuilder.Entity<Invoice>().HasData(invoices);
            modelBuilder.Entity<InvoiceItem>().HasData(invoiceItems);
            #endregion
        }
    }
}