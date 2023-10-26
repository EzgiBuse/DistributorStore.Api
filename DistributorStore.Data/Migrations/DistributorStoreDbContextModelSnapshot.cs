﻿// <auto-generated />
using System;
using DistributorStore.Data.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DistributorStore.Data.Migrations
{
    [DbContext(typeof(DistributorStoreDbContext))]
    partial class DistributorStoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DistributorStore.Data.Domain.Dealer", b =>
                {
                    b.Property<int>("DealerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DealerID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("BillingInfo")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("DealerName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Limit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<double>("ProfitMargin")
                        .HasColumnType("float");

                    b.HasKey("DealerID");

                    b.ToTable("Dealer", "dbo");

                    b.HasData(
                        new
                        {
                            DealerID = 1,
                            Address = "Address1",
                            BillingInfo = "BillingInfo1",
                            DealerName = "Dealer1",
                            Limit = 10000m,
                            ProfitMargin = 0.10000000000000001
                        },
                        new
                        {
                            DealerID = 2,
                            Address = "Address2",
                            BillingInfo = "BillingInfo2",
                            DealerName = "Dealer2",
                            Limit = 15000m,
                            ProfitMargin = 0.12
                        },
                        new
                        {
                            DealerID = 3,
                            Address = "Address3",
                            BillingInfo = "BillingInfo3",
                            DealerName = "Dealer3",
                            Limit = 14000m,
                            ProfitMargin = 0.19
                        });
                });

            modelBuilder.Entity("DistributorStore.Data.Domain.Message", b =>
                {
                    b.Property<int>("MessageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MessageID"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Receiver")
                        .HasColumnType("int");

                    b.Property<int>("Sender")
                        .HasColumnType("int");

                    b.HasKey("MessageID");

                    b.ToTable("Message", "dbo");
                });

            modelBuilder.Entity("DistributorStore.Data.Domain.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderID"));

                    b.Property<int>("DealerID")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<double>("TotalAmount")
                        .HasColumnType("float");

                    b.HasKey("OrderID");

                    b.ToTable("Order", "dbo");

                    b.HasData(
                        new
                        {
                            OrderID = 1,
                            DealerID = 1,
                            OrderDate = new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PaymentMethod = 1,
                            Status = 0,
                            TotalAmount = 500.0
                        },
                        new
                        {
                            OrderID = 2,
                            DealerID = 2,
                            OrderDate = new DateTime(2023, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PaymentMethod = 0,
                            Status = 1,
                            TotalAmount = 800.0
                        },
                        new
                        {
                            OrderID = 3,
                            DealerID = 2,
                            OrderDate = new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PaymentMethod = 2,
                            Status = 1,
                            TotalAmount = 700.0
                        },
                        new
                        {
                            OrderID = 4,
                            DealerID = 2,
                            OrderDate = new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PaymentMethod = 0,
                            Status = 1,
                            TotalAmount = 600.0
                        },
                        new
                        {
                            OrderID = 5,
                            DealerID = 2,
                            OrderDate = new DateTime(2023, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PaymentMethod = 0,
                            Status = 1,
                            TotalAmount = 750.0
                        });
                });

            modelBuilder.Entity("DistributorStore.Data.Domain.OrderDetails", b =>
                {
                    b.Property<int>("OrderDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderDetailID"));

                    b.Property<int>("OrderID")
                        .HasColumnType("int");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderDetailID");

                    b.ToTable("OrderDetails", "dbo");

                    b.HasData(
                        new
                        {
                            OrderDetailID = 1,
                            OrderID = 1,
                            ProductID = 1,
                            Quantity = 2
                        },
                        new
                        {
                            OrderDetailID = 2,
                            OrderID = 1,
                            ProductID = 2,
                            Quantity = 3
                        },
                        new
                        {
                            OrderDetailID = 3,
                            OrderID = 2,
                            ProductID = 3,
                            Quantity = 1
                        },
                        new
                        {
                            OrderDetailID = 4,
                            OrderID = 2,
                            ProductID = 1,
                            Quantity = 4
                        },
                        new
                        {
                            OrderDetailID = 5,
                            OrderID = 3,
                            ProductID = 2,
                            Quantity = 2
                        },
                        new
                        {
                            OrderDetailID = 6,
                            OrderID = 4,
                            ProductID = 1,
                            Quantity = 4
                        },
                        new
                        {
                            OrderDetailID = 7,
                            OrderID = 5,
                            ProductID = 2,
                            Quantity = 2
                        });
                });

            modelBuilder.Entity("DistributorStore.Data.Domain.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductID"));

                    b.Property<int>("MinimumStock")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("StockQuantity")
                        .HasColumnType("int");

                    b.HasKey("ProductID");

                    b.ToTable("Product", "dbo");

                    b.HasData(
                        new
                        {
                            ProductID = 1,
                            MinimumStock = 20,
                            Price = 50m,
                            ProductName = "Product1",
                            StockQuantity = 100
                        },
                        new
                        {
                            ProductID = 2,
                            MinimumStock = 30,
                            Price = 70m,
                            ProductName = "Product2",
                            StockQuantity = 150
                        },
                        new
                        {
                            ProductID = 3,
                            MinimumStock = 50,
                            Price = 80m,
                            ProductName = "Product3",
                            StockQuantity = 200
                        });
                });

            modelBuilder.Entity("DistributorStore.Data.Domain.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("UserID");

                    b.ToTable("User", "dbo");

                    b.HasData(
                        new
                        {
                            UserID = 11,
                            Password = "admin123",
                            Role = 0,
                            UserName = "admin1"
                        },
                        new
                        {
                            UserID = 1,
                            Password = "dealer123",
                            Role = 1,
                            UserName = "dealer1"
                        },
                        new
                        {
                            UserID = 2,
                            Password = "dealer456",
                            Role = 1,
                            UserName = "dealer2"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}