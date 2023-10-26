using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace DistributorStore.Data.Domain
{
    [Table("Product", Schema = "dbo")]
    public class Product
    {
        
        public int ProductID { get; set; }

       
        public string ProductName { get; set; }

        
        public double Price { get; set; }

       
        public int StockQuantity { get; set; }

        
        public int MinimumStock { get; set; }
    }
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //Configuration of product table
            builder.HasKey(p => p.ProductID);
            builder.Property(p => p.ProductName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)"); // Adjust precision and scale as needed
            builder.Property(p => p.StockQuantity).IsRequired();
           
            builder.Property(p => p.MinimumStock).IsRequired();

            
        }
    }
}
