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
    [Table("OrderDetails", Schema = "dbo")]
    public class OrderDetails
    {
        //OrderDetails database table
        public int OrderDetailID { get; set; }

       
        public int OrderID { get; set; }

       
        public int ProductID { get; set; }

        
        public int Quantity { get; set; }
    }
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetails>
    {
        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            //OrderDetails table configuration
            builder.HasKey(od => od.OrderDetailID);
            builder.Property(od => od.OrderID).IsRequired();
            builder.Property(od => od.ProductID).IsRequired();
            builder.Property(od => od.Quantity).IsRequired();
           

            
        }
    }
}
