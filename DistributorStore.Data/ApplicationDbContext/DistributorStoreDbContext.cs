using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DistributorStore.Data.Domain;
using Microsoft.EntityFrameworkCore;


namespace DistributorStore.Data.ApplicationDbContext
{
    public class DistributorStoreDbContext : DbContext
    {
        public DistributorStoreDbContext(DbContextOptions<DistributorStoreDbContext> options) : base(options)
        {
        }
        //Creating DbSets for the models
        public DbSet<User> Users { get; set; }
        public DbSet<Dealer> Dealers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Message> Messages { get; set; }

        // Configuring the entities
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //applying configurations for the tables
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new DealerConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());

            
            base.OnModelCreating(modelBuilder);
            //Calling the method for seeding data into the database tables
            DataSeeder.SeedData(modelBuilder);
        }

    }

    public static class DataSeeder
    {
        //seeding Data into the database tables
        public static void SeedData(ModelBuilder modelBuilder)
        {
            // Seed data for Dealers table
            modelBuilder.Entity<Dealer>().HasData(
                new Dealer
                {
                    DealerID = 1,
                    DealerName = "Dealer1",
                    Address = "Address1",
                    BillingInfo = "BillingInfo1",
                    Limit = 10000,
                    ProfitMargin = 0.1
                },
                new Dealer
                {
                    DealerID = 2,
                    DealerName = "Dealer2",
                    Address = "Address2",
                    BillingInfo = "BillingInfo2",
                    Limit = 15000,
                    ProfitMargin = 0.12
                },
                new Dealer
                {
                    DealerID = 3,
                    DealerName = "Dealer3",
                    Address = "Address3",
                    BillingInfo = "BillingInfo3",
                    Limit = 14000,
                    ProfitMargin = 0.19
                }
                
            );

            // Seed data for Orders table
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    OrderID = 1,
                    DealerID = 1, // Foreign key referencing a dealer
                    OrderDate = new DateTime(2023, 10, 1),
                    Status = OrderStatus.WaitingforApproval,
                    PaymentMethod = PaymentMethods.CreditCard,
                    TotalAmount = 500
                },
                new Order
                {
                    OrderID = 2,
                    DealerID = 2, // Foreign key referencing a dealer
                    OrderDate = new DateTime(2023, 10, 2),
                    Status = OrderStatus.Approved,
                    PaymentMethod = PaymentMethods.EFT,
                    TotalAmount = 800
                },
                new Order
                {
                    OrderID = 3,
                    DealerID = 2, // Foreign key referencing a dealer
                    OrderDate = new DateTime(2023, 10, 1),
                    Status = OrderStatus.Approved,
                    PaymentMethod = PaymentMethods.Balance,
                    TotalAmount = 700
                },
                new Order
                {
                    OrderID = 4,
                    DealerID = 2, // Foreign key referencing a dealer
                    OrderDate = new DateTime(2023, 10, 1),
                    Status = OrderStatus.Approved,
                    PaymentMethod = PaymentMethods.EFT,
                    TotalAmount = 600
                },
                new Order
                {
                    OrderID = 5,
                    DealerID = 2, // Foreign key referencing a dealer
                    OrderDate = new DateTime(2023, 8, 22),
                    Status = OrderStatus.Approved,
                    PaymentMethod = PaymentMethods.EFT,
                    TotalAmount = 750
                }
            );
            // Seed data for OrderDetails table
        modelBuilder.Entity<OrderDetails>().HasData(
            new OrderDetails
            {
                OrderDetailID = 1,
                OrderID = 1, // Foreign key referencing an order
                ProductID = 1, // Foreign key referencing a product
                Quantity = 2
            },
            new OrderDetails
            {
                OrderDetailID = 2,
                OrderID = 1, // Foreign key referencing an order
                ProductID = 2, // Foreign key referencing a product
                Quantity = 3
            },
            // Add more order details as needed
            new OrderDetails
            {
                OrderDetailID = 3,
                OrderID = 2, // Foreign key referencing an order
                ProductID = 3, // Foreign key referencing a product
                Quantity = 1
            },
            new OrderDetails
            {
                OrderDetailID = 4,
                OrderID = 2, // Foreign key referencing an order
                ProductID = 1, // Foreign key referencing a product
                Quantity = 4
            },
            new OrderDetails
            {
                OrderDetailID = 5,
                OrderID = 3, // Foreign key referencing an order
                ProductID = 2, // Foreign key referencing a product
                Quantity = 2
            },
             new OrderDetails
             {
                 OrderDetailID = 6,
                 OrderID = 4, // Foreign key referencing an order
                 ProductID = 1, // Foreign key referencing a product
                 Quantity = 4
             },
            new OrderDetails
            {
                OrderDetailID = 7,
                OrderID = 5, // Foreign key referencing an order
                ProductID = 2, // Foreign key referencing a product
                Quantity = 2
            }
                );
            // Seed data for Products table
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductID = 1,
                    ProductName = "Product1",
                    Price = 50,
                    StockQuantity = 100,
                    MinimumStock = 20
                },
                new Product
                {
                    ProductID = 2,
                    ProductName = "Product2",
                    Price = 70,
                    StockQuantity = 150,
                    MinimumStock = 30
                },
                new Product
                {
                    ProductID = 3,
                    ProductName = "Product3",
                    Price = 80,
                    StockQuantity = 200,
                    MinimumStock = 50
                }
                
            );
            // Seed data for Users table
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserID = 11,
                    UserName = "admin1",
                    Password = "admin123",
                    Role = UserRole.Admin
                },
                new User
                {
                    UserID = 1,
                    UserName = "dealer1",
                    Password = "dealer123",
                    Role = UserRole.Dealer
                },
                new User
                {
                    UserID = 2,
                    UserName = "dealer2",
                    Password = "dealer456",
                    Role = UserRole.Dealer
                }
            );
        }
    }
}
