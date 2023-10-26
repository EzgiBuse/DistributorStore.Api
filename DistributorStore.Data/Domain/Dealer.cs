using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributorStore.Data.Domain
{
    [Table("Dealer", Schema = "dbo")]
    public class Dealer
    {
        //Dealer table in database
        public int DealerID { get; set; }

       
        public string DealerName { get; set; }

        
        public string Address { get; set; }

        
        public string BillingInfo { get; set; }

        
        public decimal Limit { get; set; }

        
        public double ProfitMargin { get; set; }
    }

    public class DealerConfiguration : IEntityTypeConfiguration<Dealer>
    {
        public void Configure(EntityTypeBuilder<Dealer> builder)
        {
            // Configuration for Dealer table in database
            builder.HasKey(d => d.DealerID);
            builder.Property(d => d.DealerName).IsRequired().HasMaxLength(100);
            builder.Property(d => d.Address).IsRequired().HasMaxLength(200);
            builder.Property(d => d.BillingInfo).IsRequired().HasMaxLength(200);
            builder.Property(d => d.Limit).IsRequired();
            builder.Property(d => d.ProfitMargin).IsRequired();
           
           
               

        }
    }
}
