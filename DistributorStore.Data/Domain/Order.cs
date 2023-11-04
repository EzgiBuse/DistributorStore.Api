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
    [Table("Order",Schema ="dbo")]
    public class Order
    {
       //Order table in database
        public int OrderID { get; set; }

       
        public int DealerID { get; set; }

        public DateTime OrderDate { get; set; }

       
        public OrderStatus Status { get; set; }

       
        public PaymentMethods PaymentMethod { get; set; }

        
        public double TotalAmount { get; set; }
    }

    //Enum for displaying different payment methods
    public enum PaymentMethods
    {
        EFT,    
        CreditCard,
        Balance
    }

    //Enum for displaying order statusses
    public enum OrderStatus
    {
        WaitingforApproval,
        Approved,
        Cancelled

    }
    //Enum for checking wanted report window
    public enum ReportWindow
    {
        Daily,
        Weekly,
        Monthly

    }
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            //Order table configuration in database
            builder.HasKey(o => o.OrderID);
            builder.Property(o => o.DealerID).IsRequired();
            builder.Property(o => o.OrderDate);
            builder.Property(o => o.Status).IsRequired();
            builder.Property(o => o.PaymentMethod).IsRequired();
            builder.Property(o => o.TotalAmount).IsRequired(); 
            
        }
    }
}
